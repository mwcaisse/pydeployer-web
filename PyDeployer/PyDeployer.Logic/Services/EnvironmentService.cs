using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OwlTin.Common;
using OwlTin.Common.Exceptions;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;

namespace PyDeployer.Logic.Services
{
    public class EnvironmentService
    {

        private readonly PyDeployerDbContext _db;

        public EnvironmentService(PyDeployerDbContext db)
        {
            this._db = db;
        }


        public Common.Entities.Environment Get(long id)
        {
            return _db.Environments.Active().FirstOrDefault(e => e.EnvironmentId == id);
        }

        public Common.Entities.Environment GetByUuid(string uuid)
        {
            var guid = new Guid(uuid);
            return _db.Environments.Active().FirstOrDefault(e => e.EnvironmentUuid == guid);
        }

        public IEnumerable<Common.Entities.Environment> GetAll()
        {
            return _db.Environments.Active().ToList();
        }

        public Common.Entities.Environment Create(EnvironmentViewModel toCreate)
        {
            if (string.IsNullOrEmpty(toCreate.Name))
            {
                throw new EntityValidationException("Environmemnt Name cannot be blank!");
            }

            var env = new Common.Entities.Environment()
            {
                Name = toCreate.Name,
                HostName = toCreate.HostName,
                EnvironmentUuid = Guid.NewGuid(),
                Active = true
            };

            _db.Environments.Add(env);
            _db.SaveChanges();

            return env;
        }

        public Common.Entities.Environment Update(EnvironmentViewModel toUpdate)
        {
            var env = Get(toUpdate.EnvironmentId);

            if (string.IsNullOrEmpty(toUpdate.Name))
            {
                throw new EntityValidationException("Environmemnt Name cannot be blank!");
            }

            env.Name = toUpdate.Name;
            env.HostName = toUpdate.HostName;

            return env;
        }

        public bool Delete(long id)
        {
            var env = GetOrException(id);

            env.Active = false;
            _db.SaveChanges();

            return true;
        }

        protected Common.Entities.Environment GetOrException(long id,
            string exceptionText = "Environment does not exist!")
        {
            var env = Get(id);

            if (null == env)
            {
                throw new EntityValidationException(exceptionText);
            }

            return env;
        }

    }
}
