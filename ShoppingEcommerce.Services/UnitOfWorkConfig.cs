using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingEcommerce.DataAccess;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Common;

namespace ShoppingEcommerce.Services
{
    public interface IUnitOfWorkConfig : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
    public class UnitOfWorkConfig : IUnitOfWorkConfig
    {
        private DbContext context;
        private DbTransaction transaction;
        private ObjectContext objectContext;

        public UnitOfWorkConfig(ISurePortalConfigContext context)
        {
            this.context = context as DbContext;
        }
        public void BeginTransaction()
        {
            if (objectContext == null)
            {
                objectContext = ((IObjectContextAdapter)context).ObjectContext;
            }
            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
                transaction = objectContext.Connection.BeginTransaction();

            }
        }



        public void Commit()
        {
            try
            {
                context.SaveChanges();
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception ex)
            {
                this.Rollback();
                throw ex;
            }
        }
        public void Rollback()
        {
            transaction.Rollback();
            IEnumerable<DbEntityEntry> entries = context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case System.Data.Entity.EntityState.Modified:
                        entry.State = System.Data.Entity.EntityState.Unchanged;
                        break;
                    case System.Data.Entity.EntityState.Added:
                        entry.State = System.Data.Entity.EntityState.Detached;
                        break;
                    case System.Data.Entity.EntityState.Deleted:
                        entry.State = System.Data.Entity.EntityState.Unchanged;
                        break;
                }
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (objectContext != null)
                    {
                        if (objectContext.Connection.State == ConnectionState.Open)
                        {
                            objectContext.Connection.Close();
                        }
                    }
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }

}
