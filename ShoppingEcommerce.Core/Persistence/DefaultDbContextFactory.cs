using System;
using System.Data.Entity;

namespace ShoppingEcommerce.Core.Persistence
{
    public class DefaultDbContextFactory : IDbContextFactory
    {
        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return Activator.CreateInstance<TDbContext>();
        }
    }
}