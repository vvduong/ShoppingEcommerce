using System;
using System.Data;

namespace ShoppingEcommerce.Core.Persistence
{
    public class DbContextScopeFactory
    {
        private readonly IDbContextFactory _defaultDbContextFactory;

        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _defaultDbContextFactory = dbContextFactory;
        }

        public DbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextScope(joiningOption, false, null, _defaultDbContextFactory);
        }

        public DbContextReadOnlyScope CreateReadOnly(
            DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextReadOnlyScope(joiningOption, null, _defaultDbContextFactory);
        }

        public DbContextScope CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextScope(DbContextScopeOption.ForceCreateNew, false, isolationLevel,
                _defaultDbContextFactory);
        }

        public DbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextReadOnlyScope(DbContextScopeOption.ForceCreateNew, isolationLevel,
                _defaultDbContextFactory);
        }

        public IDisposable SuppressAmbientDbContext()
        {
            return new AmbientDbContextSuppressor();
        }
    }
}