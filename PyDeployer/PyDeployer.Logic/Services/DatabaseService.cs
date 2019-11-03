using System.Collections.Generic;
using System.Linq;
using OwlTin.Common;
using OwlTin.Common.Exceptions;
using PyDeployer.Common.Encryption;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;

namespace PyDeployer.Logic.Services
{
    public class DatabaseService
    {

        private readonly PyDeployerDbContext _db;
        private readonly AesEncrypter _encrypter;

        public DatabaseService(PyDeployerDbContext db, AesEncrypter encrypter)
        {
            this._db = db;
            this._encrypter = encrypter;
        }

        public Database Get(long id, bool decrypt = true)
        {
            var database = _db.Databases.Active().FirstOrDefault(d => d.DatabaseId == id);
            database?.Decrypt(_encrypter);

            return database;
        }

        public IEnumerable<Database> GetAllForEnvironment(long environmentId)
        {
            return _db.Databases.Active().Where(d => d.EnvironmentId == environmentId)
                .AsEnumerable().Decrypt(_encrypter);
        }
        
        public IEnumerable<Database> GetAll()
        {
            return _db.Databases.Active().AsEnumerable().Decrypt(_encrypter);
        }

        public Database Create(DatabaseViewModel toCreate)
        {
            ValidationUtils.ValidateViewModel(toCreate);
            
            var database = new Database()
            {
                Name = toCreate.Name,
                Type = toCreate.Type,
                Active = true,
                EncryptionKey = _encrypter.GenerateKey()
            };
            
            AddEncryptedValues(database, toCreate);

            _db.Databases.Add(database);
            _db.SaveChanges();

            return database.Decrypt(_encrypter);
        }

        public Database Update(DatabaseViewModel toUpdate)
        {
            var database = GetOrException(toUpdate.DatabaseId);
            
            ValidationUtils.ValidateViewModel(toUpdate);

            database.Name = toUpdate.Name;
            database.Type = toUpdate.Type;
            AddEncryptedValues(database, toUpdate);

            _db.SaveChanges();

            return database.Decrypt(_encrypter);
        }

        public void Delete(long id)
        {
            var database = GetOrException(id);

            database.Active = false;
            _db.SaveChanges();
        }

        /// <summary>
        /// Encrypts the unencrypted values on the viewmodel and sets them on the entity
        /// </summary>
        /// <param name="database"></param>
        /// <param name="viewModel"></param>
        
        private void AddEncryptedValues(Database database, DatabaseViewModel viewModel)
        {
            database.Host = _encrypter.Encrypt(viewModel.Host, database.EncryptionKey);
            database.Port = _encrypter.Encrypt(viewModel.Port, database.EncryptionKey);
            database.User = _encrypter.Encrypt(viewModel.User, database.EncryptionKey);
            database.Password = _encrypter.Encrypt(viewModel.Password, database.EncryptionKey);
        }
        
        private Database GetOrException(long id, string exceptionText = "Database does not exist!", bool decypt = false)
        {
            var database = Get(id, decypt);

            if (null == database)
            {
                throw new EntityValidationException(exceptionText);
            }

            return database;
        }
    }
}