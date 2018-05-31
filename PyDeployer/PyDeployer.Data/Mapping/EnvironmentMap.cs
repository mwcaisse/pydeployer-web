using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Common.Data;
using Environment = PyDeployer.Common.Entities.Environment;

namespace PyDeployer.Data.Mapping
{
    public class EnvironmentMap : IEntityTypeConfiguration<Common.Entities.Environment>
    {
        public void Configure(EntityTypeBuilder<Environment> builder)
        {

            builder.ToTable("ENVIRONMENT")
                .HasKey(e => e.EnvironmentId);

            builder.Property(e => e.EnvironmentId)
                .HasColumnName("ENVIRONMENT_ID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EnvironmentUuid)
                .HasColumnName("ENVIRONMENT_UUID")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.HostName)
                .HasColumnName("HOST_NAME")
                .IsRequired()
                .HasMaxLength(100);
            
            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
