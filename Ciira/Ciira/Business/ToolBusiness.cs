using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace Ciira.Business
{
    public class ToolBusiness
    {

        public static String UniqueId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static String ShortUniqueId()
        {
            char[] _base62chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

            Random _random = new Random();

            int length = 15;

            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
                sb.Append(_base62chars[_random.Next(62)]);

            return sb.ToString();
        }

        public static string EncryptCookieText(String cookie)
        {
            String key = "5602509fce617560320aa1c65b560290bc3d827560720c808b2b";
            return Encrypt(cookie, key);
        }

        public static string DecryptCookieText(String cookie)
        {
            String key = "5602509fce617560320aa1c65b560290bc3d827560720c808b2b";
            return Decrypt(cookie, key);
        }

        public static string EncryptPassword(String password)
        {
            String key = "5602209fce617560220aa1c65b560220bc3d827560220c808b2b";
            return Encrypt(password, key);
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("56021fe5ecfb4ffb");
            // This constant is used to determine the keysize of the encryption algorithm.
            const int keysize = 256;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("56021fe5ecfb4ffb");
            // This constant is used to determine the keysize of the encryption algorithm.
            const int keysize = 256;

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

    }
}