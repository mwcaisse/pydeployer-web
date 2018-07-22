﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mitchell.Authentication.Entities;
using Mitchell.Common.Data;

namespace Mitchell.Authentication.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER")
                .HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .HasColumnName("USERNAME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("PASSWORD")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("NAME")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("USER_EMAIL")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(u => u.Locked)
                .HasColumnName("LOCKED")
                .IsRequired();

            builder.Property(u => u.ExpirationDate)
                .HasColumnName("EXPIRATION_DATE");

            builder.Property(u => u.PasswordExpirationDate)
                .HasColumnName("PASSWORD_EXPIRATION_DATE");

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();


        }
    }
}
