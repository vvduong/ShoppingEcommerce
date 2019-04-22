namespace ShoppingEcommerce.Services
{ 
    public interface ICertificateService
    {
        bool VerifyCertificatePassword(byte[] certificate, string password);
    }
}
