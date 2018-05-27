using System;
using Microsoft.EntityFrameworkCore;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data
{
    public class PyDeployerDbContext : DbContext
    {

        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationEnvironment> ApplicationEnvironments { get; set; }

        public DbSet<ApplicationEnvironmentToken> ApplicationEnvironmentTokens { get; set; }

        public DbSet<ApplicationToken> ApplicationTokens { get; set; }

        public DbSet<Common.Entities.Environment> Environments { get; set; }

        public PyDeployerDbContext(DbContextOptions<PyDeployerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TOOD: IMPLEMENT MY MODEL MAPPERS
        }

    }
}
