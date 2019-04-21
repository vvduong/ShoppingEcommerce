using System;
using ShoppingEcommerce.DataAccess;
using System.Data.Entity;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Unity.Attributes;
using System.Security.Principal;
using System.Collections.Generic;

namespace ShoppingEcommerce.Services
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public SurePortalContext context;
        private DbContextTransaction transaction;
        //private readonly IUnityContainer unityContainer;

        private Hashtable services { get; set; }
        private bool isDisposed;

        //[Dependency]
        //protected IIdentity Identity { get; set; }

        //[Dependency]
        //protected HttpRequest HttpRequest { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UnitOfWork(SurePortalContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Thay đổi connection của context đang sử dụng
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool ChangeConnection(string connectionString)
        {
            // Phuong comment
            this.context.Database.Connection.ConnectionString = connectionString;
            return true;
        }

        public TContext GetContext<TContext>() where TContext : class
        {
            return this.context as TContext;
        }
        //public TService GetService<TService>()
        //{
        //    // Validate setting
        //    if (services == null)
        //        services = new Hashtable();
        //    // validate the exist state of the Entity in the repository by the Name.
        //    var serviceType = typeof(TService).Name;
        //    if (services.ContainsKey(serviceType))
        //    {
        //        return (TService)services[serviceType];
        //    }
        //    // Gets the type of service.
        //    var service = unityContainer.Resolve<TService>();
        //    if (!services.ContainsKey(serviceType))
        //    {
        //        services.Add(serviceType, service);
        //    }
        //    return (TService)service;
        //}
        public void BeginTransaction()
        {
            if (this.transaction == null)
            {
                transaction = this.context.Database.BeginTransaction();
            }
        }
      
        public void Commit()
        {
            try
            {
                if (this.transaction == null)
                {
                    throw new Exception("You must begin transaction");
                }

                //#region SYSTEM LOG

                //var addedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Added)
                //    .ToList();

                //SystemLog(addedEntities, EntityState.Added);

                //var modifiedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Modified)
                //    .ToList();

                //SystemLog(modifiedEntities, EntityState.Modified);

                //var deletedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Deleted)
                //    .ToList();

                //SystemLog(deletedEntities, EntityState.Deleted);

                //#endregion

                this.context.SaveChanges();

                if (this.transaction != null)
                {
                    this.transaction.Commit();
                    this.transaction = null;
                }
            }
            catch (Exception ex) {
                Rollback();
                throw ex;

            }
        }
        void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // Make sure only one dispose
            if (isDisposed)
            {
                GC.SuppressFinalize(this);
            }
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                this.context.Dispose();
            }

            isDisposed = true;
        }

        //private void SystemLog(IEnumerable<DbEntityEntry> entities, EntityState entityState)
        //{
        //    var identity = Identity != null ? Identity.Name : string.Empty;

        //    var ipAddress = HttpRequest != null && HttpRequest.ServerVariables.HasKeys() 
        //        ? HttpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"] 
        //        : string.Empty;

        //    var flag = false;

        //    if (!string.IsNullOrEmpty(ipAddress))
        //    {
        //        var ipAddresses = ipAddress.Split(',');

        //        if (ipAddresses.Length != 0)
        //        {
        //            ipAddress = ipAddresses[0];

        //            flag = true;
        //        }
        //    }

        //    if (!flag)
        //    {
        //        ipAddress = HttpRequest != null && HttpRequest.ServerVariables.HasKeys() 
        //            ? HttpRequest.ServerVariables["REMOTE_ADDR"] 
        //            : string.Empty;
        //    }

        //    var area = string.Empty;

        //    if (HttpRequest != null && HttpRequest.RequestContext.RouteData.DataTokens["area"] != null)
        //    {
        //        area = HttpRequest.RequestContext.RouteData.DataTokens["area"].ToString();
        //    }

        //    var ignoreProperties = new[]
        //    {
        //        "created", "authorid", "modified", "editorid",
        //        "createddate", "createdby", "updateddate", "updatedby"
        //    };

        //    var systemLogs = new List<SystemLog>();

        //    foreach (var entity in entities)
        //    {
        //        if (entityState == EntityState.Modified)
        //        {
        //            foreach (var propertyName in entity.OriginalValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var currentValue = entity.CurrentValues[propertyName];

        //                var originalValue = entity.OriginalValues[propertyName];

        //                if (originalValue == currentValue)
        //                {
        //                    continue;
        //                }

        //                systemLogs.Add(new SystemLog
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Source = entity.Entity.GetType().Name,
        //                    Key = propertyName,
        //                    Action = entityState.ToString(),
        //                    Object = currentValue != null ? currentValue.ToString() : string.Empty,
        //                    Computer = ipAddress,
        //                    Module = area,
        //                    Author = identity,
        //                    Created = DateTime.Now
        //                });
        //            }
        //        }
        //        if (entityState == EntityState.Added)
        //        {
        //            foreach (var propertyName in entity.CurrentValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var currentValue = entity.CurrentValues[propertyName];

        //                if (currentValue != null && !string.IsNullOrEmpty(currentValue.ToString()))
        //                {
        //                    systemLogs.Add(new SystemLog
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        Source = entity.Entity.GetType().Name,
        //                        Key = propertyName,
        //                        Action = entityState.ToString(),
        //                        Object = currentValue.ToString(),
        //                        Computer = ipAddress,
        //                        Module = area,
        //                        Author = identity,
        //                        Created = DateTime.Now
        //                    });
        //                }
        //            }
        //        }
        //        if (entityState == EntityState.Deleted)
        //        {
        //            foreach (var propertyName in entity.OriginalValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var originalValue = entity.OriginalValues[propertyName];

        //                if (originalValue != null && !string.IsNullOrEmpty(originalValue.ToString()))
        //                {
        //                    systemLogs.Add(new SystemLog
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        Source = entity.Entity.GetType().Name,
        //                        Key = propertyName,
        //                        Action = entityState.ToString(),
        //                        Object = originalValue.ToString(),
        //                        Computer = ipAddress,
        //                        Module = area,
        //                        Author = identity,
        //                        Created = DateTime.Now
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    context.Configuration.AutoDetectChangesEnabled = false;
        //    context.Configuration.ValidateOnSaveEnabled = false;
        //    context.SystemLogs.AddRange(systemLogs);
        //}
    }


    public partial class DocUnitOfWork : IDocUnitOfWork
    {
        public SurePortalContext context;
        private DbContextTransaction transaction;
        //private readonly IUnityContainer unityContainer;
        private Hashtable services { get; set; }
        private bool isDisposed;

        //[Dependency]
        //protected IIdentity Identity { get; set; }

        //[Dependency]
        //protected HttpRequest HttpRequest { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocUnitOfWork(/*IUnityContainer unityContainer*/)
        {
            this.context = new SurePortalContext();
            //this.unityContainer = unityContainer;
        }

        //public TService GetService<TService>()
        //{
        //    // Validate setting
        //    if (services == null)
        //        services = new Hashtable();
        //    // validate the exist state of the Entity in the repository by the Name.
        //    var serviceType = typeof(TService).Name;
        //    if (services.ContainsKey(serviceType))
        //    {
        //        return (TService)services[serviceType];
        //    }
        //    // Gets the type of service.
        //    var service = unityContainer.Resolve<TService>();
        //    if (!services.ContainsKey(serviceType))
        //    {
        //        services.Add(serviceType, service);
        //    }
        //    return (TService)service;
        //}
        public TContext GetContext<TContext>() where TContext : class
        {
            return this.context as TContext;
        }

        /// <summary>
        /// Thay đổi connection của context đang sử dụng
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool ChangeConnection(string connectionString)
        {
            // Phuong comment
            this.context.Database.Connection.Close();
            this.context.Database.Connection.ConnectionString = connectionString;
            return true;
        }
        
        public void BeginTransaction()
        {
            if (this.transaction == null)
            {
                transaction = this.context.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            try
            {
                if (this.transaction == null)
                {
                    throw new Exception("You must begin transaction");
                }

                //#region SYSTEM LOG

                //var addedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Added)
                //    .ToList();

                //SystemLog(addedEntities, EntityState.Added);

                //var modifiedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Modified)
                //    .ToList();

                //SystemLog(modifiedEntities, EntityState.Modified);

                //var deletedEntities = context.ChangeTracker
                //    .Entries()
                //    .Where(predicate => predicate.State == EntityState.Deleted)
                //    .ToList();

                //SystemLog(deletedEntities, EntityState.Deleted);

                //#endregion

                this.context.SaveChanges();

                if (this.transaction != null)
                {
                    this.transaction.Commit();
                    this.transaction = null;
                }
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;

            }
        }
        void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // Make sure only one dispose
            if (isDisposed)
            {
                GC.SuppressFinalize(this);
            }
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                this.context.Dispose();
            }

            isDisposed = true;
        }

        //private void SystemLog(IEnumerable<DbEntityEntry> entities, EntityState entityState)
        //{
        //    var identity = Identity != null ? Identity.Name : string.Empty;

        //    var ipAddress = HttpRequest != null && HttpRequest.ServerVariables.HasKeys() 
        //        ? HttpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"] 
        //        : string.Empty;

        //    var flag = false;

        //    if (!string.IsNullOrEmpty(ipAddress))
        //    {
        //        var ipAddresses = ipAddress.Split(',');

        //        if (ipAddresses.Length != 0)
        //        {
        //            ipAddress = ipAddresses[0];

        //            flag = true;
        //        }
        //    }

        //    if (!flag)
        //    {
        //        ipAddress = HttpRequest != null && HttpRequest.ServerVariables.HasKeys() 
        //            ? HttpRequest.ServerVariables["REMOTE_ADDR"] 
        //            : string.Empty;
        //    }

        //    var area = string.Empty;

        //    if (HttpRequest != null && HttpRequest.RequestContext.RouteData.DataTokens["area"] != null)
        //    {
        //        area = HttpRequest.RequestContext.RouteData.DataTokens["area"].ToString();
        //    }

        //    var ignoreProperties = new [] { "created", "authorid", "modified", "editorid" };

        //    var systemLogs = new List<SystemLog>();

        //    foreach (var entity in entities)
        //    {
        //        if (entityState == EntityState.Modified)
        //        {
        //            foreach (var propertyName in entity.OriginalValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var currentValue = entity.CurrentValues[propertyName];

        //                var originalValue = entity.OriginalValues[propertyName];

        //                if (originalValue == currentValue)
        //                {
        //                    continue;
        //                }

        //                systemLogs.Add(new SystemLog
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Source = entity.Entity.GetType().Name,
        //                    Key = propertyName,
        //                    Action = entityState.ToString(),
        //                    Object = currentValue != null ? currentValue.ToString() : string.Empty,
        //                    Computer = ipAddress,
        //                    Module = area,
        //                    Author = identity,
        //                    Created = DateTime.Now
        //                });
        //            }
        //        }
        //        if (entityState == EntityState.Added)
        //        {
        //            foreach (var propertyName in entity.CurrentValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var currentValue = entity.CurrentValues[propertyName];

        //                if (currentValue != null && !string.IsNullOrEmpty(currentValue.ToString()))
        //                {
        //                    systemLogs.Add(new SystemLog
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        Source = entity.Entity.GetType().Name,
        //                        Key = propertyName,
        //                        Action = entityState.ToString(),
        //                        Object = currentValue.ToString(),
        //                        Computer = ipAddress,
        //                        Module = area,
        //                        Author = identity,
        //                        Created = DateTime.Now
        //                    });
        //                }
        //            }
        //        }
        //        if (entityState == EntityState.Deleted)
        //        {
        //            foreach (var propertyName in entity.OriginalValues.PropertyNames)
        //            {
        //                if (ignoreProperties.Contains(propertyName.ToLower()))
        //                {
        //                    continue;
        //                }

        //                var originalValue = entity.OriginalValues[propertyName];

        //                if (originalValue != null && !string.IsNullOrEmpty(originalValue.ToString()))
        //                {
        //                    systemLogs.Add(new SystemLog
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        Source = entity.Entity.GetType().Name,
        //                        Key = propertyName,
        //                        Action = entityState.ToString(),
        //                        Object = originalValue.ToString(),
        //                        Computer = ipAddress,
        //                        Module = area,
        //                        Author = identity,
        //                        Created = DateTime.Now
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    context.Configuration.AutoDetectChangesEnabled = false;
        //    context.Configuration.ValidateOnSaveEnabled = false;
        //    context.SystemLogs.AddRange(systemLogs);
        //}
    }
}