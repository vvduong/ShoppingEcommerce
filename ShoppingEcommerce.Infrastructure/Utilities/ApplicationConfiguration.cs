using System.Configuration;

namespace ShoppingEcommerce.Utilities
{
    public static class ApplicationConfiguration
    {
        public static string AdDomain => ConfigurationManager.AppSettings["ADDomain"];
    }
}