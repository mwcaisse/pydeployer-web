using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PyDeployer.Common.Encryption
{
    public class AesEncrypter
    {

        /// <summary>
        ///  Creates an encryption key with the given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GenerateKey(int length=256)
        {
            byte[] key = new byte[length];
            RandomNumberGenerator.Create().GetBytes(key);
            return key;
        }

        public string Encrypt(string plainText, byte[] key)
        {

            byte[] encrypted;
            byte[] IV;
            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(key, aes.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }

                            encrypted = msEncrypt.ToArray();
                            IV = aes.IV;
                        }
                    }
                }
            }

            return EncodeCipher(IV, encrypted);

        }

        public string Decrypt(string cipherText, byte[] key)
        {
            string plainText;
            dynamic decoded = DecodeCipher(cipherText);

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = decoded.IV;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var msDecrypt = new MemoryStream(decoded.Encrypted))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return plainText;
        }

        /// <summary>
        /// Encode the IV and encrypted text into base64 encoded cipher
        /// </summary>
        /// <param name="IV"></param>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        protected string EncodeCipher(byte[] IV, byte[] encrypted)
        {
            return String.Format("{0}_{1}",
                Convert.ToBase64String(IV),
                Convert.ToBase64String(encrypted));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodedCipher"></param>
        /// <returns></returns>
        protected object DecodeCipher(string encodedCipher)
        {
            var tokens = encodedCipher.Split("_");
            if (tokens.Length != 2)
            {
                throw new EncryptionException("Cannot decode. Invalid format, too many tokens!");
            }
            return new
            {
                IV = Convert.FromBase64String(tokens[0]),
                Encrypted = Convert.FromBase64String(tokens[1])
            };
        }

    }
}
