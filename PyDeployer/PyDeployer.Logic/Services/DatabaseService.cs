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

        public Database Get(long id)
        {
            return _db.Databases.Active().FirstOrDefault(d => d.DatabaseId == id);
        }

        public IEnumerable<Database> GetAllForEnvironment(long environmentId)
        {
            return _db.Databases.Active().Where(d => d.EnvironmentId == environmentId);
        }

        public Database Create(DatabaseViewModel toCreate)
        {
            //TODO: Handle validation
            
            var database = new Database()
            {
                Name = toCreate.Name,
                Type = toCreate.Type,
                //TODO: Handle encryption of these fields
                Host = toCreate.Host,
                Port = toCreate.Port,
                User = toCreate.User,
                Password = toCreate.Password,
                Active = true,
                EncryptionKey = _encrypter.GenerateKey()
            };

            _db.Databases.Add(database);
            _db.SaveChanges();

            return database;
        }

        public Database Update(DatabaseViewModel toUpdate)
        {
            var database = GetOrException(toUpdate.DatabaseId);
            
            //TODO: Handle validation

            database.Name = toUpdate.Name;
            database.Type = toUpdate.Type;
            database.Host = toUpdate.Host;
            database.Port = toUpdate.Port;
            database.User = toUpdate.User;
            database.Password = toUpdate.Password;

            _db.SaveChanges();
            
            return database;
        }

        public IEnumerable<Database> GetAll()
        {
            return _db.Databases.Active();
        }

        public void Delete(long id)
        {
            var database = GetOrException(id);

            database.Active = false;
            _db.SaveChanges();
        }

        protected Database GetOrException(long id, string exceptionText = "Database does not exist!")
        {
            var database = Get(id);

            if (null == database)
            {
                throw new EntityValidationException(exceptionText);
            }

            return database;
        }
    }
}