using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OwlTin.Common;
using OwlTin.Common.Exceptions;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;

namespace PyDeployer.Logic.Services
{
    public class BuildTokenService
    {

        private readonly PyDeployerDbContext _db;

        public BuildTokenService(PyDeployerDbContext db)
        {
            this._db = db;
        }

        public BuildToken Get(long id)
        {
            return _db.BuildTokens.Active().FirstOrDefault(bt => bt.BuildTokenId == id);
        }

        public IEnumerable<BuildToken> GetAll()
        {
            return _db.BuildTokens.Active();
        }

        public BuildToken Create(BuildTokenViewModel toCreate)
        {
            if (string.IsNullOrWhiteSpace(toCreate.Name))
            {
                throw new EntityValidationException("Token name cannot be blank!");
            }

            if (_db.BuildTokens.Active().Any(bt => bt.Name == toCreate.Name))
            {
                throw new EntityValidationException("A build token with this name already exists!");
            }

            var token = new BuildToken()
            {
                Name = toCreate.Name,
                Value = toCreate.Value,
                Active = true
            };

            _db.BuildTokens.Add(token);
            _db.SaveChanges();

            return Get(token.BuildTokenId);
        }

        public BuildToken Update(BuildTokenViewModel toUpdate)
        {
            if (string.IsNullOrWhiteSpace(toUpdate.Name))
            {
                throw new EntityValidationException("Token name cannot be blank!");
            }

            var token = GetOrException(toUpdate.BuildTokenId);

            if (toUpdate.Name != token.Name && _db.BuildTokens.Active().Any(bt => bt.Name == toUpdate.Name))
            {
                throw new EntityValidationException("A build token with this name already exists!");
            }

            token.Name = toUpdate.Name;
            token.Value = toUpdate.Value;

            _db.SaveChanges();

            return token;
        }

        public void Delete(long id)
        {
            var token = GetOrException(id);

            token.Active = false;
            _db.SaveChanges();
        }


        protected BuildToken GetOrException(long id, 
            string exceptionText = "Build Token does not exist!")
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
