using System;
using Microsoft.EntityFrameworkCore;
using OwlTin.Authentication.Data;
using OwlTin.Authentication.Data.Mapping;
using OwlTin.Authentication.Entities;
using PyDeployer.Common.Entities;
using PyDeployer.Data.Mapping;

namespace PyDeployer.Data
{
    public class PyDeployerDbContext : DbContext, IAuthenticationDbContext
    {

        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationEnvironment> ApplicationEnvironments { get; set; }

        public DbSet<ApplicationEnvironmentToken> ApplicationEnvironmentTokens { get; set; }

        public DbSet<ApplicationToken> ApplicationTokens { get; set; }

        public DbSet<Common.Entities.Environment> Environments { get; set; }

        public DbSet<BuildToken> BuildTokens { get; set; }
        
        public DbSet<Database> Databases { get; set; }

        //Authentication Entities
        public DbSet<User> Users { get; set;  }

        public DbSet<UserAuthenticationToken> UserAuthenticationTokens { get; set;  }

        public DbSet<UserRegistrationKey> UserRegistrationKeys { get; set;  }

        public DbSet<UserRegistrationKeyUse> UserRegistrationKeyUses { get; set;  }


        public PyDeployerDbContext(DbContextOptions<PyDeployerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationMap());
            modelBuilder.ApplyConfiguration(new EnvironmentMap());
            modelBuilder.ApplyConfiguration(new ApplicationTokenMap());
            modelBuilder.ApplyConfiguration(new ApplicationEnvironmentMap());
            modelBuilder.ApplyConfiguration(new ApplicationEnvironmentTokenMap());
            modelBuilder.ApplyConfiguration(new BuildTokenMap());
            modelBuilder.ApplyConfiguration(new DatabaseMap());

            //Authentication Mappers
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserAuthenticationTokenMap());
            modelBuilder.ApplyConfiguration(new UserRegistrationKeyMap());
            modelBuilder.ApplyConfiguration(new UserRegistrationKeyUseMap());
        }

    }
}
