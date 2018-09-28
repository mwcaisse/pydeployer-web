using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OwlTin.Common.Data;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Mapping
{
    public class ApplicationEnvironmentMap : IEntityTypeConfiguration<ApplicationEnvironment>
    {
        public void Configure(EntityTypeBuilder<ApplicationEnvironment> builder)
        {
            builder.ToTable("APPLICATION_ENVIRONMENT")
                .HasKey(ae => ae.ApplicationEnvironmentId);

            builder.Property(ae => ae.ApplicationEnvironmentId)
                .HasColumnName("APPLICATION_ENVIRONMENT_ID")
                .ValueGeneratedOnAdd();

            builder.Property(ae => ae.ApplicationId)
                .HasColumnName("APPLICATION_ID")
                .IsRequired();

            builder.Property(ae => ae.EnvironmentId)
                .HasColumnName("ENVIRONMENT_ID")
                .IsRequired();

            builder.HasOne(ae => ae.Application)
                .WithMany(a => a.ApplicationEnvironments)
                .HasForeignKey(ae => ae.ApplicationId)
                .IsRequired();

            builder.HasOne(ae => ae.Environment)
                .WithMany(e => e.ApplicationEnvironments)
                .HasForeignKey(ae => ae.EnvironmentId)
                .IsRequired();

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
