using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Common.Data;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Mapping
{
    public class ApplicationMap : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {

            builder.ToTable("APPLICATION")
                .HasKey(a => a.ApplicationId);

            builder.Property(a => a.ApplicationId)
                .HasColumnName("APPLICATION_ID")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.ApplicationUuid)
                .HasColumnName("APPLICATION_UUID")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(a => a.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100);


            builder.AddActiveEntityProperties();
            builder.AddTrackedEntityProperties();
        }
    }
}
