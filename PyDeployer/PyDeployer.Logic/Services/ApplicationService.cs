using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mitchell.Common;
using Mitchell.Common.Exceptions;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;

namespace PyDeployer.Logic.Services
{
    public class ApplicationService
    {

        private readonly PyDeployerDbContext _db;

        public ApplicationService(PyDeployerDbContext db)
        {
            this._db = db;
        }

        public Application Get(long id)
        {
            return _db.Applications.Active().FirstOrDefault(a => a.ApplicationId == id);
        }

        public Application GetByUuid(string uuid)
        {
            var guid = new Guid(uuid);
            return _db.Applications.Active().FirstOrDefault(a => a.ApplicationUuid == guid);
        }

        public IEnumerable<Application> GetAll()
        {
            return _db.Applications.Active().ToList();
        }

        public Application Create(ApplicationViewModel toCreate)
        {
            if (string.IsNullOrWhiteSpace(toCreate.Name))
            {
                throw new EntityValidationException("Application Name cannot be blank!");
            }

            var app = new Application()
            {
                Name = toCreate.Name,
                ApplicationUuid = Guid.NewGuid(),
                Active = true
            };

            _db.Applications.Add(app);
            _db.SaveChanges();

            return app;
        }

        public Application Update(ApplicationViewModel toUpdate)
        {
            var app = GetOrException(toUpdate.ApplicationId);

            if (string.IsNullOrWhiteSpace(toUpdate.Name))
            {
                throw new EntityValidationException("Application Name cannot be blank!");
            }

            app.Name = toUpdate.Name;

            _db.SaveChanges();

            return app;
        }

        public bool Delete(long id)
        {
            var app = GetOrException(id);

            app.Active = false;
            _db.SaveChanges();

            return true;
        }

        protected Application GetOrException(long id, 
            string exceptionText = "Application does not exist!")
        {
            var app = Get(id);

            if (null == app)
            {
                throw new EntityValidationException(exceptionText);
            }

            return app;
        }

    }
}
