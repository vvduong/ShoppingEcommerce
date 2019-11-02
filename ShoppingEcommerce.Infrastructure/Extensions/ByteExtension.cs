using System.Text;

namespace ShoppingEcommerce.Extensions
{
    public static class ByteExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToString(this byte[] value)
        {
            return Encoding.Default.GetString(value);
        }
    }
}