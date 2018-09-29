using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OwlTin.Common.Data;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Mapping
{
    public class BuildTokenMap : IEntityTypeConfiguration<BuildToken>
    {
        public void Configure(EntityTypeBuilder<BuildToken> builder)
        {
            builder.ToTable("BUILD_TOKEN")
                .HasKey(bt => bt.BuildTokenId);

            builder.Property(bt => bt.BuildTokenId)
                .HasColumnName("BUILD_TOKEN_ID")
                .ValueGeneratedOnAdd();

            builder.Property(bt => bt.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(bt => bt.Value)
                .HasColumnName("TOKEN_VALUE")
                .IsRequired()
                .HasMaxLength(255);

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
