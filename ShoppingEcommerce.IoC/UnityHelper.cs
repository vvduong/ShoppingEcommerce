using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using ShoppingEcommerce.Core.Persistence;
using ShoppingEcommerce.Core.Repository;
using ShoppingEcommerce.Core.Tasks;
using ShoppingEcommerce.Mapper;
using Unity;
using Unity.Injection;
using Unity.log4net;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace LacViet.SurePortal.IoC
{
    public static class UnityHelper
    {
        public static IUnityContainer BuildUnityContainer(IUnityContainer container)
        {
            // logging framework
            container.AddNewExtension<Log4NetExtension>();

            // special case for factory method
            container.RegisterType<IDbContextFactory, DefaultDbContextFactory>(new PerResolveLifetimeManager());

            // register all your components with the container
            // it is NOT necessary to register controllers here
            // prefer using conventions as much as possible
            var types = AllClasses
                .FromLoadedAssemblies()
                .Where(predicate => predicate.Namespace != null
                                    && (predicate.Namespace.StartsWith("LacViet")
                                        || predicate.Namespace.StartsWith("SurePortal"))
                                    && !predicate.Namespace.Contains("Framework")
                                    && !string.IsNullOrEmpty(predicate.AssemblyQualifiedName)
                                    && !predicate.AssemblyQualifiedName.Contains(
                                        "LacViet.SurePortal.Business.OrganizationChart")
                                    && !predicate.AssemblyQualifiedName.Contains(
                                        "LacViet.SurePortal.Business.BusinessDocument")
                                    && !typeof(Controller).IsAssignableFrom(predicate)
                                    && !predicate.IsAbstract
                                    && !predicate.IsInterface)
                .ToList();

            #region HOOK REGISTERED TASKS INTO GLOBAL EVENTS

            var taskTypes = types
                .Where(predicate => typeof(IExecuteOnStart).IsAssignableFrom(predicate)
                                    || typeof(IExecuteOnError).IsAssignableFrom(predicate)
                                    || typeof(IExecuteOnBeginRequest).IsAssignableFrom(predicate)
                                    || typeof(IExecuteOnEndRequest).IsAssignableFrom(predicate)
                                    || typeof(IUnityDependencyResolver).IsAssignableFrom(predicate))
                .ToList();

            container.RegisterTypes(taskTypes,
                WithMappings.FromAllInterfaces,
                WithName.TypeName,
                WithLifetime.Transient);

            #endregion

            #region HEADER INTERFACES AND CONCRETES IMPLEMENT

            container.RegisterTypes(types,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);

            #endregion

            #region AUTOMAPPER

            MappingRegistration(container, types);

            #endregion

            // abstraction layers should placed in core
            // not in presentation
            // TODO;
            // we have this line because BaseService need ITimeSheetRepository<> as its dependency
            // and unity can not resolve ITimeSheetRepository<> when it ignore all abstract generic by default
            // so we must do this manualy
            // need another solution here, this line should be DELETE
            container.RegisterType<SurePortalContext, SurePortalContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new PerResolveLifetimeManager());
            
            return container;
        }

        private static void MappingRegistration(IUnityContainer container, IList<Type> types)
        {
            try
            {
                var maps = types
                    .SelectMany(collectionSelector => collectionSelector
                            .GetInterfaces()
                            .Where(predicate =>
                                predicate.IsGenericType && predicate.GetGenericTypeDefinition() == typeof(IMapping<>)),
                        (type, itype) => new
                        {
                            Source = itype.GetGenericArguments().First(),
                            Destination = type
                        })
                    .ToArray();

                var complexMaps = types
                    .SelectMany(collectionSelector => collectionSelector
                            .GetInterfaces()
                            .Where(predicate => typeof(IComplexMapping).IsAssignableFrom(predicate)),
                        (type, itype) => Activator.CreateInstance(type) as IComplexMapping)
                    .ToArray();

                container
                    .RegisterType<MapperConfiguration>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc =>
                            new MapperConfiguration(configure =>
                            {
                                configure.ConstructServicesUsing(constructor => factoryFunc.Resolve(constructor));

                                foreach (var map in maps)
                                    configure
                                        .CreateMap(map.Source, map.Destination)
                                        .ReverseMap();

                                foreach (var map in complexMaps) map.CreateMap(configure);
                            })))
                    .RegisterType<IConfigurationProvider>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc => factoryFunc.Resolve<MapperConfiguration>()))
                    .RegisterType<IMapper>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc => factoryFunc
                            .Resolve<MapperConfiguration>()
                            .CreateMapper()));
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                var stringBuilder = new StringBuilder();

                foreach (var loaderException in reflectionTypeLoadException.LoaderExceptions)
                {
                    stringBuilder.AppendLine(loaderException.Message);

                    if (loaderException is FileNotFoundException fileNotFoundException)
                        if (!string.IsNullOrEmpty(fileNotFoundException.FusionLog))
                            stringBuilder.AppendLine(fileNotFoundException.FusionLog);

                    stringBuilder.AppendLine();
                }

                Debug.Write(stringBuilder.ToString());
            }
        }
    }
}