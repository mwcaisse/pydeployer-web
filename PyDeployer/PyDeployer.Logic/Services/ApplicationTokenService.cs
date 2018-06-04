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
    public class ApplicationTokenService
    {

        private readonly PyDeployerDbContext _db;

        public ApplicationTokenService(PyDeployerDbContext db)
        {
            this._db = db;
        }

        public ApplicationToken Get(long id)
        {
            return _db.ApplicationTokens.Active().FirstOrDefault(t => t.ApplicationTokenId == id);
        }

        public IEnumerable<ApplicationToken> GetForApplication(long applicationId)
        {
            return _db.ApplicationTokens.Active().Where(t => t.ApplicationId == applicationId);
        }

        public ApplicationToken Create(ApplicationTokenViewModel toCreate)
        {
            if (string.IsNullOrEmpty(toCreate.Name))
            {
                throw new EntityValidationException("Token Name cannot be blank!");
            }
            if (!ApplicationExists(toCreate.ApplicationId))
            {
                throw new EntityValidationException("Application does not exist!");
            }

            if (_db.ApplicationTokens.Active()
                .Any(at => at.ApplicationId == toCreate.ApplicationId && at.Name == toCreate.Name))
            {
                throw new EntityValidationException("A token with this name already exists for this application.");
            }

            var token = new ApplicationToken()
            {
                Name = toCreate.Name,
                ApplicationId = toCreate.ApplicationId,
                Active = true
            };

            _db.ApplicationTokens.Add(token);
            _db.SaveChanges();

            return token;

        }

        public ApplicationToken Update(ApplicationTokenViewModel toUpdate)
        {
            if (string.IsNullOrEmpty(toUpdate.Name))
            {
                throw new EntityValidationException("Token Name cannot be blank!");
            }

            var token = GetOrException(toUpdate.ApplicationTokenId);

            if (token.ApplicationId != toUpdate.ApplicationId)
            {
                throw new EntityValidationException("You cannot change the Application of the Token after creation!");
            }

            token.Name = toUpdate.Name;

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

        protected bool ApplicationExists(long applicationId)
        {
            return _db.Applications.Active().Any(a => a.ApplicationId == applicationId);
        }

        protected ApplicationToken GetOrException(long id,
            string exceptionText = "Application Token does not exist!")
        {
            var token = Get(id);

            if (null == token)
            {
                throw new EntityValidationException(exceptionText);
            }

            return token;
        }

    }
}
