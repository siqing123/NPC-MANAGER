using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    // LC: missing the RSA part.
    public class AES
    {
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }

        public AES()
        {
            IV = new byte[16];
            Key = new byte[16];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(IV);
                rng.GetBytes(Key);
            }
        }

        public AES(byte[] iv, byte[] key)
        {
            IV = iv;
            Key = key;
        }

        public string Encrypt(string plainText)
        {
            string cipherText = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    byte[] cipherRaw = ms.ToArray();
                    cipherText = Convert.ToBase64String(cipherRaw);

                }
            }
                return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            string plainText = string.Empty;
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptyor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptyor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }

                return plainText;
        }
    }
}
