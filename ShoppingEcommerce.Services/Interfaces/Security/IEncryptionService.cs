
namespace ShoppingEcommerce.Services
{
    public interface IEncryptionService 
    {
        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        string CreateSaltKey(int size);

        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">{assword</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="passwordFormat">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");

        /// <summary>
        /// Create a data hash
        /// </summary>
        /// <param name="data">The data for calculating the hash</param>
        /// <param name="hashAlgorithm">Hash algorithm</param>
        /// <returns>Data hash</returns>
        string CreateHash(byte [] data, string hashAlgorithm = "SHA1");

        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        string EncryptText(string plainText, string encryptionPrivateKey = "");

        /// <summary>
        /// Encrypt file
        /// </summary>
        /// <param name="file">File to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted file</returns>
        byte[] EncryptFile(byte[] file, string encryptionPrivateKey = "");

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        string DecryptText(string cipherText, string encryptionPrivateKey = "");
        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        string Encrypt(string toDecrypt, string key, bool useHashing);
        /// <summary>
        /// Decrypt password
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        string Decrypt(string toDecrypt, string key, bool useHashing);
        /// <summary>
        /// Decrypt file
        /// </summary>
        /// <param name="file">file to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted file</returns>
        byte[] DecryptFile(byte[] file, string encryptionPrivateKey = "");
        /// <summary>
        /// Get MD5Hash
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string GetMD5HashData(string data);
    }
}
