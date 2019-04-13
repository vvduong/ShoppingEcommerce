using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Unity;

namespace ShoppingEcommerce.Web
{
    public class UnitySubResolver : ISubDependencyResolver
    {
        private readonly IUnityContainer _unityContainer;

        public UnitySubResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return _unityContainer.Registrations
                .Any(registration => registration.RegisteredType == dependency.TargetType);
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return _unityContainer.Resolve(dependency.TargetType);
        }
    }
}