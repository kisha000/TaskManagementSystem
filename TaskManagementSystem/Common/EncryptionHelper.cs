using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagementSystem.Common
{
    public class EncryptionHelper
    {
        // Ensure the key is at least 32 bytes long for AES-256 encryption
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("6mHr3H7dK8sN2pQsT5wW8zZ1C4fXjAmP");

        // Generate a random IV for each encryption operation
        private static byte[] GenerateIV()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        public static string EncryptQueryString(string queryString)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(queryString);

            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.IV = GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    byte[] encryptedBytes = ms.ToArray();
                    byte[] iv = aes.IV;

                    byte[] result = new byte[iv.Length + encryptedBytes.Length];
                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }

        public static string DecryptQueryString(string encryptedQueryString)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedQueryString);

            // Extract IV from the beginning of the encrypted data
            byte[] iv = new byte[16];
            Array.Copy(cipherBytes, iv, iv.Length);

            // Extract encrypted data excluding IV
            byte[] encryptedBytes = new byte[cipherBytes.Length - iv.Length];
            Array.Copy(cipherBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}