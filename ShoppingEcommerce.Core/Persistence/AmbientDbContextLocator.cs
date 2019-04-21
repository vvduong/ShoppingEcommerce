using System.Data.Entity;

namespace ShoppingEcommerce.Core.Persistence
{
    /// <summary>
    /// Convenience methods to retrieve ambient DbContext instances. 
    /// </summary>
    public class AmbientDbContextLocator
    {
        /// <summary>
        /// If called within the scope of a DbContextScope, gets or creates 
        /// the ambient DbContext instance for the provided DbContext type. 
        /// 
        /// Otherwise returns null. 
        /// </summary>
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            return DbContextScope.GetAmbientScope()?.DbContexts.Get<TDbContext>();
        }
    }
}