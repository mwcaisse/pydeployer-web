using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OwlTin.Common;
using OwlTin.Common.Exceptions;
using PyDeployer.Common.Entities;
using PyDeployer.Data;
using PyDeployer.Data.Extensions;

namespace PyDeployer.Logic.Services
{
    public class ApplicationEnvironmentService
    {

        private readonly PyDeployerDbContext _db;

        public ApplicationEnvironmentService(PyDeployerDbContext db)
        {
            this._db = db;
        }

        public ApplicationEnvironment Get(long id)
        {
            return _db.ApplicationEnvironments.Active().Build().FirstOrDefault(ae => ae.ApplicationEnvironmentId == id);
        }

        public IEnumerable<ApplicationEnvironment> GetForApplication(long applicationId)
        {
            return _db.ApplicationEnvironments.Active().Build().Where(ae => ae.ApplicationId == applicationId);
        }

        public ApplicationEnvironment Create(long applicationId, long environmentId)
        {
            if (!ApplicationExists(applicationId))
            {
                throw new EntityValidationException("Application does not exist!");
            }
            if (!EnvironmentExists(environmentId))
            {
                throw new EntityValidationException("Environment does not exist!");
            }

            if (
                _db.ApplicationEnvironments.Active()
                    .Any(ae => ae.ApplicationId == applicationId && ae.EnvironmentId == environmentId))
            {
                throw new EntityValidationException("This environment is already added to this application.");
            }

            var env = new ApplicationEnvironment()
            {
                ApplicationId = applicationId,
                EnvironmentId =  environmentId,
                Active = true
            };

            _db.ApplicationEnvironments.Add(env);
            _db.SaveChanges();

            return Get(env.ApplicationEnvironmentId);
        }

        public bool Delete(long applicationId, long environmentId)
        {
            var env =
                _db.ApplicationEnvironments.Active()
                    .FirstOrDefault(ae => ae.ApplicationId == applicationId && ae.EnvironmentId == environmentId);

            if (null == env)
            {
                throw new EntityValidationException("This enviroment is not added to this application");
            }

            env.Active = false;
            _db.SaveChanges();

            return true;
        }

        protected ApplicationEnvironment GetOrException(long id,
            string exceptionText = "Application Environment doesn't exist")
        {
            var environment =
                _db.ApplicationEnvironments.Active().FirstOrDefault(ae => ae.ApplicationEnvironmentId == id);

            if (null == environment)
            {
                throw new EntityValidationException(exceptionText);
            }

            return environment;
        }

        protected bool ApplicationExists(long applicationId)
        {
            return _db.Applications.Active().Any(a => a.ApplicationId == applicationId);
        }

        protected bool EnvironmentExists(long environmentId)
        {
            return _db.Environments.Active().Any(e => e.EnvironmentId == environmentId);
        }

    }
}
