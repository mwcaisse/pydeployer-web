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
    public class ApplicationEnvironmentTokenService
    {

        private readonly PyDeployerDbContext _db;

        public ApplicationEnvironmentTokenService(PyDeployerDbContext db)
        {
            this._db = db;
        }

        public ApplicationEnvironmentToken Get(long id)
        {
            return _db.ApplicationEnvironmentTokens.Active()
                .FirstOrDefault(t => t.ApplicationEnvironmentTokenId == id);
        }

        public IEnumerable<ApplicationEnvironmentToken> GetForEnvironment(long applicationId, 
            long environmentId)
        {
            return
                _db.ApplicationEnvironmentTokens.Active()
                    .Where(
                        t =>
                            t.ApplicationEnvironment.ApplicationId == applicationId &&
                            t.ApplicationEnvironment.EnvironmentId == environmentId);
        }

        public IEnumerable<ApplicationEnvironmentToken> GetForEnvironmentByUuid(
            string applicationUuid, string environmentUuid)
        {
            var applicationGuid = new Guid(applicationUuid);
            var environmentGuid = new Guid(environmentUuid);
            return
                _db.ApplicationEnvironmentTokens.Active()
                    .Where(
                        t =>
                            t.ApplicationEnvironment.Application.ApplicationUuid == applicationGuid &&
                            t.ApplicationEnvironment.Environment.EnvironmentUuid == environmentGuid);
        }

        public ApplicationEnvironmentToken Create(ApplicationEnvironmentTokenViewModel toCreate)
        {
            if (!ApplicationExists(toCreate.ApplicationId))
            {
                throw new EntityValidationException("Application does not exist!");
            }
            if (!EnvironmentExists(toCreate.EnvironmentId))
            {
                throw new EntityValidationException("Environment does not exist!");
            }
            if (!ApplicationEnvironmentExists(toCreate.ApplicationId, toCreate.EnvironmentId))
            {
                throw new EntityValidationException("The application isn't configured for this environment.");
            }
            if (!ApplicationTokenExists(toCreate.ApplicationTokenId))
            {
                throw new EntityValidationException("The application does not have a definition for this token");
            }


            var applicationEnvironment =
                _db.ApplicationEnvironments.Active()
                    .First(
                        ae => ae.ApplicationId == toCreate.ApplicationId && 
                        ae.EnvironmentId == toCreate.EnvironmentId);

            var token = new ApplicationEnvironmentToken()
            {
                ApplicationEnvironment = applicationEnvironment,
                ApplicationTokenId = toCreate.ApplicationTokenId,
                Value = toCreate.Value,
                Active = true
            };

            _db.ApplicationEnvironmentTokens.Add(token);
            _db.SaveChanges();

            return token;
        }

        public ApplicationEnvironmentToken Update(ApplicationEnvironmentTokenViewModel toUpdate)
        {
            var token = GetOrException(toUpdate.ApplicationEnvironmentTokenId);

            if (token.ApplicationEnvironment.EnvironmentId != toUpdate.EnvironmentId ||
                token.ApplicationEnvironment.ApplicationId != toUpdate.ApplicationId)
            {
                throw new EntityValidationException("Cannot change the Application/Environment the token is associated with.");
            }
            if (token.ApplicationTokenId != toUpdate.ApplicationTokenId)
            {
                throw new EntityValidationException("Cannot change the Application token this value is associated with.");
            }

            token.Value = toUpdate.Value;
            _db.SaveChanges();

            return token;
        }

        public bool Delete(long id)
        {
            var token = GetOrException(id);

            token.Active = false;
            _db.SaveChanges();

            return true;
        }

        protected ApplicationEnvironmentToken GetOrException(long id,
            string exceptionText = "Token does not exist!")
        {
            var token = Get(id);

            if (null == token)
            {
                throw new EntityValidationException(exceptionText);
            }

            return token;
        }

        protected bool ApplicationExists(long applicationId)
        {
            return _db.Applications.Active().Any(a => a.ApplicationId == applicationId);
        }

        protected bool EnvironmentExists(long environmentId)
        {
            return _db.Environments.Active().Any(e => e.EnvironmentId == environmentId);
        }

        protected bool ApplicationEnvironmentExists(long applicationId, long environmentId)
        {
            return
                _db.ApplicationEnvironments.Active()
                    .Any(ae => ae.ApplicationId == applicationId && ae.EnvironmentId == environmentId);
        }

        protected bool ApplicationTokenExists(long applicationTokenId)
        {
            return _db.ApplicationTokens.Active().Any(t => t.ApplicationTokenId == applicationTokenId);
        }

    }
}
