using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingEcommerce.Core.MobileApiModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SurePortalEncrypting
    {
        #region Rijndae
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.        
        private const string InitVector = "tr@n!m1nh!9u65n!";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int Keysize = 128;

        public const string PassSalt = "Biov3Salt";

        /// <summary>
        /// Thực hiện mã hóa password
        /// </summary>
        /// <param name="plainText">password cần mã hóa</param>
        /// <param name="passSalt">khóa ngẫu nhiên</param>
        /// <returns></returns>
        public static string EncryptRijndae(string plainText, string passSalt)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passSalt, null);
            byte[] keyBytes = password.GetBytes(Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Thực hiện giải mã password
        /// </summary>
        /// <param name="cipherText">password cần giải mã</param>
        /// <param name="passSalt">khóa ngẫu nhiên</param>
        /// <returns></returns>
        public static string DecryptRijndae(string cipherText, string passSalt)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passSalt, null);
            byte[] keyBytes = password.GetBytes(Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /// <summary>
        /// Hàm tạo khóa ngẫu nhiên password
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        /// <summary>
        /// Hàm mã hóa password sử dụng dành cho ASPNET lưu trữ
        /// </summary>
        /// <param name="type">loại mã hóa SHA256</param>
        /// <param name="pass">mật khẩu</param>
        /// <param name="salt">khóa ngẫu nhiên</param>
        /// <returns></returns>
        public static string EncodePasswordForASPNET(string type, string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            byte[] inArray = null;
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create(type);
            inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        #endregion
        
        #region RSA

        public static string RSAEncryption(string publicKey, string strText)
        {
            var testData = Encoding.Default.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    //get a stream from the string
                    var sr = new System.IO.StringReader(publicKey);
                    //we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //get the object back from the stream
                    var pubKey = (RSAParameters)xs.Deserialize(sr);
                    // client encrypting data with public key issued by server                    
                    rsa.ImportParameters(pubKey);

                    var encryptedData = rsa.Encrypt(testData, false);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                catch (Exception e)
                {
                    return "";
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string RSADecryption(string privateKey, string strText)
        {
            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;

                    //get a stream from the string
                    var sr = new System.IO.StringReader(privateKey);
                    //we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //get the object back from the stream
                    var privKey = (RSAParameters)xs.Deserialize(sr);
                    // server decrypting data with private key                 
                    rsa.ImportParameters(privKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        #endregion

        #region AES
        public static string EncryptAES(string clearText)
        {
            string encryptionKey = ConfigurationManager.AppSettings["EncryptKey"];
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

            using (var encryptor = new AesCryptoServiceProvider())
            {
                encryptor.KeySize = 128;
                encryptor.BlockSize = 128;
                //// ios will use the same 16 byte from key for IV
                var keyBytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["EncryptSalt"]);
                var secretKeyBytes = Encoding.UTF8.GetBytes(encryptionKey);
                //Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));

                encryptor.Key = secretKeyBytes;
                encryptor.IV = keyBytes; 
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = BitConverter.ToString(ms.ToArray()).Replace("-", string.Empty); // Convert.ToBase64String(ms.ToArray()); // 
                }
            }
            return clearText;
        }

        public static string DecryptAES(string cipherText, bool isHexString = true)
        {
            string encryptionKey = ConfigurationManager.AppSettings["EncryptKey"];
            byte[] cipherBytes = null;
            if (isHexString)
            {
                cipherBytes = HexStringToByteArray(cipherText); 
            }
            else
            {
                cipherBytes = Convert.FromBase64String(cipherText); 
            }
            using (var encryptor = new AesCryptoServiceProvider())
            {
                encryptor.BlockSize = 128;
                encryptor.KeySize = 128;
                // ios will use the same 16 byte from key for IV
                var keyBytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["EncryptSalt"]);
                var secretKeyBytes = Encoding.UTF8.GetBytes(encryptionKey);
                encryptor.Key = secretKeyBytes;
                encryptor.IV = keyBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    byte[] byteArray = ms.ToArray();
            
                    cipherText = Encoding.UTF8.GetString(byteArray);  //Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string bigToLittle(string data)
        {
            long sValueAsInt = long.Parse(data, System.Globalization.NumberStyles.HexNumber);
            byte[] bytes = BitConverter.GetBytes(sValueAsInt);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval; //output {652E47D2F9EEAB8B}
        }

        public static byte[] NetworkToHostOrder(byte[] array, int offset, int length)
        {
            return array.Skip(offset).Take(length).Reverse().ToArray();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ConvertToLittleEndian(string input)
        {
            string result = "";
            int idx = 0;
            while (idx <= input.Length)
            {
                if (idx + 1 <= input.Length)
                {
                    result += LittleEndian(input.Substring(idx, 2));
                }
                idx += 2;
            }
            return result;
        }


        static string LittleEndian(string num)
        {
            int number = Convert.ToInt32(num, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval;
        }

        #endregion
    }
}
