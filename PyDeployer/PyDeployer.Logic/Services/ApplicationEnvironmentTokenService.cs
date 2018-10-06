using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OwlTin.Common;
using OwlTin.Common.Exceptions;
using PyDeployer.Common.Encryption;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;
using PyDeployer.Data.Extensions;

namespace PyDeployer.Logic.Services
{
    public class ApplicationEnvironmentTokenService
    {

        private readonly PyDeployerDbContext _db;
        private readonly AesEncrypter _encrypter;

        public ApplicationEnvironmentTokenService(PyDeployerDbContext db, AesEncrypter encrypter)
        {
            this._db = db;
            this._encrypter = encrypter;
        }

        public IEnumerable<ApplicationEnvironmentTokenViewModel> GetForEnvironment(long applicationId, 
            long environmentId)
        {
            var tokens =  (from at in _db.ApplicationTokens.Active().Build()
                    join aet in
                        _db.ApplicationEnvironmentTokens.Active()
                            .Where(ae => ae.ApplicationEnvironment.EnvironmentId == environmentId)
                    on new {at.ApplicationTokenId, environmentId} 
                    equals new {aet.ApplicationTokenId, environmentId=aet.ApplicationEnvironment.EnvironmentId} into aetj
                    from aet in aetj.DefaultIfEmpty()
                    where at.ApplicationId == applicationId
                    select new ApplicationEnvironmentTokenViewModel()
                    {
                        ApplicationId = applicationId,
                        EnvironmentId = environmentId,
                        Name = at.Name,
                        Value = (null == aet) ? null : aet.Value,
                        ApplicationEnvironmentTokenId = (null == aet) ? -1 : aet.ApplicationEnvironmentTokenId
                    }).ToList();

            //Decrypt all of the tokens
            foreach (var token in tokens)
            {
                if (_encrypter.IsEncrypted(token.Value))
                {
                    token.Value = _encrypter.Decrypt(token.Value, GetEncryptionKey(token.ApplicationId));
                }
            }
       
            return tokens;

        }

        public IEnumerable<ApplicationEnvironmentTokenViewModel> GetForEnvironmentByUuid(
            string applicationUuid, string environmentUuid)
        {
            var applicationGuid = new Guid(applicationUuid);
            var environmentGuid = new Guid(environmentUuid);

            var applicationEnvironment = _db.ApplicationEnvironments.Active().Build()
                .FirstOrDefault(ae => ae.Application.ApplicationUuid == applicationGuid
                                   && ae.Environment.EnvironmentUuid == environmentGuid);

            if (null == applicationEnvironment)
            {
                throw new EntityValidationException("The given Application Environment mapping does not exist!");
            }


            return GetForEnvironment(applicationEnvironment.ApplicationId, applicationEnvironment.EnvironmentId);
        }

        public ApplicationEnvironmentToken Save(ApplicationEnvironmentTokenViewModel toCreate)
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
            var applicationToken =
                _db.ApplicationTokens.Active()
                    .FirstOrDefault(x => x.ApplicationId == toCreate.ApplicationId && x.Name == toCreate.Name);
            if (null == applicationToken)
            {
                throw new EntityValidationException("The application does not have a definition for this token");
            }

            var environmentToken = _db.ApplicationEnvironmentTokens.Active()
                .FirstOrDefault(ae => ae.ApplicationTokenId == applicationToken.ApplicationTokenId 
                                   && ae.ApplicationEnvironment.EnvironmentId == toCreate.EnvironmentId);

            var encryptedValue = _encrypter.Encrypt(toCreate.Value, GetEncryptionKey(toCreate.ApplicationId));

            if (null == environmentToken)
            {
                var applicationEnvironment =
                    _db.ApplicationEnvironments.Active()
                        .First(
                            ae => ae.ApplicationId == toCreate.ApplicationId &&
                                  ae.EnvironmentId == toCreate.EnvironmentId);

                environmentToken = new ApplicationEnvironmentToken()
                {
                    ApplicationEnvironment = applicationEnvironment,
                    ApplicationTokenId = applicationToken.ApplicationTokenId,
                    Value = encryptedValue,
                    Active = true
                };

                _db.ApplicationEnvironmentTokens.Add(environmentToken);
            }
            else
            {
                environmentToken.Value = encryptedValue;
            }
            
            _db.SaveChanges();
            return Get(environmentToken.ApplicationEnvironmentTokenId, true);
        }

        public bool Delete(long id)
        {
            var token = GetOrException(id);

            token.Active = false;
            _db.SaveChanges();

            return true;
        }

        protected ApplicationEnvironmentToken Get(long id, bool decrypt = false)
        {
            var token = _db.ApplicationEnvironmentTokens.Active().Build()
                .FirstOrDefault(t => t.ApplicationEnvironmentTokenId == id);

            if (null != token && decrypt && _encrypter.IsEncrypted(token.Value))
            {
                token.Value = _encrypter.Decrypt(token.Value, GetEncryptionKey(token.ApplicationToken.ApplicationId));
            }

            return token;
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

        protected string GetEncryptionKey(long applicationId)
        {
            var application = _db.Applications.First(a => a.ApplicationId == applicationId);
            if (null == application.EncryptionKey)
            {
                application.EncryptionKey = _encrypter.GenerateKey();
                _db.SaveChanges();
            }

            return application.EncryptionKey;
        }
        
    }
}
