using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PyDeployer.Common.Encryption;

namespace PyDeployer.Data.Extensions
{
    public static class MappingExtensions
    {

        public static void AddEncryptionContextEntityProperties<T>(this EntityTypeBuilder<T> builder)
            where T : class, IEncryptionContextEntity
        {
            builder.Property(e => e.EncryptionKey)
                .HasColumnName("ENCRYPTION_KEY")
                .HasMaxLength(1000);
        }
        
    }
}