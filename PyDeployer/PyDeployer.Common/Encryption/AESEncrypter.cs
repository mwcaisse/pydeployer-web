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
        /// <param name="length">Number of bits to use. Must be multiple of 8</param>
        /// <returns></returns>
        public virtual string GenerateKey(int length=256)
        {
            byte[] key = new byte[length/ 8];
            RandomNumberGenerator.Create().GetBytes(key);
            return EncodeKey(key);
        }

        public virtual bool IsEncrypted(string value)
        {
            return !string.IsNullOrEmpty(value) && value.StartsWith(EncodedCipherPrefix);
        }

        /// <summary>
        /// Encrypts the given plain text using the given encryption key
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string Encrypt(string plainText, string key)
        {

            byte[] encrypted;
            byte[] IV;
            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(DecodeKey(key), aes.IV))
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

        /// <summary>
        ///  Decryptes the given cipher text using the given encryption key
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string Decrypt(string cipherText, string key)
        {
            string plainText;
            dynamic decoded = DecodeCipher(cipherText);

            using (var aes = Aes.Create())
            {
                aes.Key = DecodeKey(key);
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

        protected static readonly string EncodedCipherPrefix = "__aese__";

        /// <summary>
        /// Encode the IV and encrypted text into base64 encoded cipher
        /// </summary>
        /// <param name="IV"></param>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        protected string EncodeCipher(byte[] IV, byte[] encrypted)
        {
            return string.Format("{0}{1}|{2}",
                EncodedCipherPrefix,
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
            if (!encodedCipher.StartsWith(EncodedCipherPrefix))
            {
                throw new EncryptionException("Could not decrypt! Invalid format.");
            }

            var tokens = encodedCipher.Substring(EncodedCipherPrefix.Length).Split("|");
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

        protected static readonly string EncodedKeyPrefix = "__aes__";

        protected string EncodeKey(byte[] key)
        {
            return string.Format("{0}{1}", EncodedKeyPrefix, Convert.ToBase64String(key));
        }

        protected byte[] DecodeKey(string encodedKey)
        {
            if (encodedKey.StartsWith(EncodedKeyPrefix))
            {
                return Convert.FromBase64String(encodedKey.Substring(EncodedKeyPrefix.Length));
            }
            throw new EncryptionException("Could not decode key! Invalid format.");
        }
    }
}
