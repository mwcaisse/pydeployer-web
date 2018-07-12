using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Common.Data;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Mapping
{
    public class ApplicationEnvironmentTokenMap : IEntityTypeConfiguration<ApplicationEnvironmentToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationEnvironmentToken> builder)
        {

            builder.ToTable("APPLICATION_ENVIRONMENT_TOKEN")
                .HasKey(ate => ate.ApplicationEnvironmentTokenId);

            builder.Property(ate => ate.ApplicationEnvironmentTokenId)
                .HasColumnName("APPLICATION_ENVIRONMENT_TOKEN_ID")
                .ValueGeneratedOnAdd();

            builder.Property(ate => ate.ApplicationEnvironmentId)
                .HasColumnName("APPLICATiON_ENVIRONMENT_ID")
                .IsRequired();

            builder.Property(ate => ate.ApplicationTokenId)
                .HasColumnName("APPLICATION_TOKEN_ID")
                .IsRequired();

            builder.Property(ate => ate.Value)
                .HasColumnName("TOKEN_VALUE")
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(ate => ate.ApplicationEnvironment)
                .WithMany(ae => ae.ApplicationEnvironmentTokens)
                .HasForeignKey(ate => ate.ApplicationEnvironmentId)
                .IsRequired();

            builder.HasOne(ate => ate.ApplicationToken)
                .WithMany(at => at.ApplicationEnvironmentTokens)
                .HasForeignKey(ate => ate.ApplicationTokenId)
                .IsRequired();
                

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
