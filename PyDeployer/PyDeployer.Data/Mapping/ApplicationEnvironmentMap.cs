using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Common.Data;
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

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
