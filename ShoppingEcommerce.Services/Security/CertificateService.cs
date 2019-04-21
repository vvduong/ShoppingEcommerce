using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ShoppingEcommerce.Services
{
    public class CertificateService : ICertificateService
    {
        public bool VerifyCertificatePassword(byte[] certificate, string password)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(certificate, password);
            }
            catch (CryptographicException ex)
            {
                if ((ex.HResult & 0xFFFF) == 0x56)
                {
                    return false;
                };
            }
            return true;
        }
    }
}
