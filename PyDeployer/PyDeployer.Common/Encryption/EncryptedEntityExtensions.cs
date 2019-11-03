using System.Collections.Generic;
using System.Linq;

namespace PyDeployer.Common.Encryption
{
    public static class EncryptedEntityExtensions
    {

        public static T Decrypt<T>(this T entity, AesEncrypter encrypter, bool failSilently = false) where T : IEncryptionContextEntity
        {
            var properties = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string) && 
                                                                  p.IsDefined(typeof(EncryptedAttribute), false));

            foreach (var property in properties)
            {
                var encryptedVal = property.GetValue(entity) as string;
                if (encrypter.IsEncrypted(encryptedVal))
                {
                    var decryptedVal = encrypter.Decrypt(encryptedVal, entity.EncryptionKey);
                    property.SetValue(entity, decryptedVal);
                }
                else if (!failSilently)
                {
                    throw new EncryptionException("Tried to decrypt a value that was not encrypted or in an " +
                                                  "unknown format!");
                }
      
            }

            return entity;
        }

        public static IEnumerable<T> Decrypt<T>(this IEnumerable<T> entities, AesEncrypter encrypter)
            where T : IEncryptionContextEntity
        {
            return entities.Select(x => x.Decrypt(encrypter));
        }
        
    }
}