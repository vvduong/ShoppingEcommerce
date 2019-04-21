using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using log4net;
using ShoppingEcommerce.Core.Utils;
using ShoppingEcommerce.DataAccess;
using Unity;


namespace ShoppingEcommerce.Core.Persistence
{
    public abstract class DocRepository<TEntity> : Repository<ShoppingCartEntities, TEntity> where TEntity : class
    {
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="ambientDbContextLocator"></param>
        /// <param name="identity"></param>
        protected DocRepository(AmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        [Dependency] protected ILog Log { get; set; }

        [Dependency] protected IIdentity Identity { get; set; }

        public void ConnectToAuthenticatedIdentityDatabase()
        {
            if (Identity == null || !Identity.IsAuthenticated || string.IsNullOrEmpty(Identity.Name))
            {
                Log.Warn("Identity is not authenticated.");

                return;
            }

            var identityName = Identity.Name;

            if (identityName.Contains("#") && identityName.Contains("|"))
            {
                identityName = GetSharePointAuthenticatedIdentityName(identityName);
            }

            if (!identityName.Contains("\\"))
            {
                identityName = ApplicationResources.ADDomain + "\\" + identityName;
            }

            //var connectionInfo =
            //DbContext
            //    .Set<UserDepartment>()
            //    .AsNoTracking()
            //    .Where(predicate => predicate.User.UserName == identityName)
            //    .Select(selector => new
            //    {
            //        selector.Department.ServerAddress,
            //        selector.Department.DatabaseName,
            //        selector.Department.UserAccess,
            //        selector.Department.Password
            //    })
            //    .FirstOrDefault();

            //if (connectionInfo == null)
            //{
            //    Log.Warn($"Can not get {identityName} connection info.");

            //    return;
            //}

            //var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            //{
            //    DataSource = connectionInfo.ServerAddress,
            //    InitialCatalog = connectionInfo.DatabaseName,
            //    UserID = connectionInfo.UserAccess,
            //    Password = connectionInfo.Password
            //};

            //DbContext.Database.Connection.Close();

            //DbContext.Database.Connection.ConnectionString = sqlConnectionStringBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        private static string GetSharePointAuthenticatedIdentityName(string identityName)
        {
            var index = identityName.IndexOf("|", StringComparison.Ordinal) + 1;

            if (index < 0)
            {
                return identityName;
            }

            var userName = identityName.Substring(index, identityName.Length - index);

            return userName.Contains("|") ? userName.Replace("|", "\\") : userName;
        }
    }
}