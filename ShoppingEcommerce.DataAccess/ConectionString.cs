using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Web;
using LacViet.ShoppingEcommerce.Utilities;

namespace LacViet.ShoppingEcommerce.DataAccess
{
    public class SingleConnection
    {
        private SingleConnection() { }
        private static SingleConnection _ConsString = null;
        private String _String = null;
        private ShoppingEcommercePrincipal _SurePrinpal = null;
        private static string _connectionStringDefault = ConfigurationManager.ConnectionStrings["ShoppingEcommerceContext"].ConnectionString;
        public static string ConString
        {
            get
            {
                if (HttpContext.Current.User is ShoppingEcommercePrincipal)
                {
                    var userCurrent = HttpContext.Current.User as ShoppingEcommercePrincipal;
                    //if (_ConsString == null)
                    //{
                    _ConsString = new SingleConnection
                    {
                        _String = SingleConnection.Connect(userCurrent.ServerName, userCurrent.DBName, userCurrent.LoginDB, userCurrent.Password)
                    };
                    return _ConsString._String;
                    //}
                    //else
                    //    return _ConsString._String;
                }
                else
                {
                    return _connectionStringDefault;
                }

            }
        }

        public static string Connect(string dataSource, string initialCatalog, string userID, string password)
        {
            //Build an SQL connection string
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource, // Server name
                InitialCatalog = initialCatalog,  //Database
                UserID = userID,         //Username
                Password = password,  //Password
                MultipleActiveResultSets = true,
                PersistSecurityInfo = true,
            };

            //Build an Entity Framework connection string
            EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                Metadata = @"res://*/ShoppingEcommerceContext.csdl|
                            res://*/ShoppingEcommerceContext.ssdl|
                            res://*/ShoppingEcommerceContext.msl",
                ProviderConnectionString = sqlString.ToString()
            };
            return entityString.ConnectionString;
        }
    }
}
