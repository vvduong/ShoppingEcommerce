using System.Configuration;

namespace ShoppingEcommerce.Infrastructure.Utilities
{
    public static class ApplicationConfiguration
    {
        public static string AdDomain => ConfigurationManager.AppSettings["ADDomain"];
    }
}