using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class EncryptionExtenders
    {
        /// <summary>
        /// encrypts data for bill pay
        /// </summary>
        /// <param name="plainText">unencrypted string</param>
        /// <param name="secretKey">a key shared with client to  decrypt data</param>
        /// <returns>tuple with Item1 being the Iv and Item2 being the encrypted string</returns>
        public static Tuple<string, string> AesEncrypt(this string plainText, string secretKey, Encoding encoding = null)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (secretKey == null || secretKey.Length <= 0)
                throw new ArgumentNullException("Key");

            byte[] IV;
            byte[] encrypted;
            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                aesAlg.Key = secretKey.GetBytes(encoding);
                IV = aesAlg.IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return new Tuple<string, string>(IV.GetString(encoding), encrypted.GetString(encoding));
        }
        /// <summary>
        /// encrypts data for bill pay
        /// </summary>
        /// <param name="plainText">unencrypted string</param>
        /// <param name="secretKey">a key shared with client to  decrypt data</param>
        /// <param name="iv">uses a pre-generated iv in case encryption needs to be done on multiple strings using the same iv</param>
        /// <returns>tuple with Item1 being the Iv and Item2 being the encrypted string</returns>
        public static Tuple<string, string> AesEncrypt(this string plainText, string secretKey, string iv, Encoding encoding = null)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (secretKey == null || secretKey.Length <= 0)
                throw new ArgumentNullException("Key");

            byte[] encrypted;
            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secretKey.GetBytes(encoding);
                aesAlg.IV = iv.GetBytes(encoding);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return new Tuple<string, string>(iv, encrypted.GetString(encoding));
        }

        /// <summary>
        /// decrypts the string given the secret key and iv
        /// </summary>
        /// <param name="cipherText">the encrypted text</param>
        /// <param name="secretKey">the secret key</param>
        /// <param name="iV">the iv</param>
        /// <returns>the unencrypted text</returns>
        public static string AesDecrypt(this string cipherText, string secretKey, string iV, Encoding encoding = null)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (secretKey == null || secretKey.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iV == null || iV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold the decrypted text. 
            string plaintext = null;

            // Create an Aes object with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secretKey.GetBytes(encoding);

                aesAlg.IV = iV.GetBytes(encoding);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText.GetBytes(encoding)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

        // Convert a string to a byte array.
        public static byte[] GetBytes(this string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(str);
        }

        public static string GetString(this byte[] stream, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(stream);
        }
    }
}
