using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Common.Data;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Mapping
{
    public class ApplicationTokenMap : IEntityTypeConfiguration<ApplicationToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationToken> builder)
        {

            builder.ToTable("APPLICATION_TOKEN")
                .HasKey(at => at.ApplicationTokenId);

            builder.Property(at => at.ApplicationTokenId)
                .HasColumnName("APPLICATION_TOKEN_ID")
                .ValueGeneratedOnAdd();

            builder.Property(at => at.ApplicationId)
                .HasColumnName("APPLICATION_ID")
                .IsRequired();

            builder.Property(at => at.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(255);
            
            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
