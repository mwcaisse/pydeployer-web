using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OwlTin.Common.Data;
using PyDeployer.Common.Entities;
using PyDeployer.Data.Extensions;

namespace PyDeployer.Data.Mapping
{
    public class DatabaseMap : IEntityTypeConfiguration<Database>
    {
        public void Configure(EntityTypeBuilder<Database> builder)
        {

            builder.ToTable("DATABASE_INSTANCE")
                .HasKey(d => d.DatabaseId);

            builder.Property(d => d.DatabaseId)
                .HasColumnName("DATABASE_ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(d => d.EnvironmentId)
                .HasColumnName("ENVIRONMENT_ID")
                .IsRequired();

            builder.Property(d => d.Name)
                .HasColumnName("NAME")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(d => d.Type)
                .HasColumnName("TYPE")
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property(d => d.Host)
                .HasColumnName("HOST")
                .HasMaxLength(500)
                .IsRequired();
            
            builder.Property(d => d.Port)
                .HasColumnName("PORT")
                .HasMaxLength(500)
                .IsRequired();
            
            builder.Property(d => d.User)
                .HasColumnName("USER")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(d => d.Password)
                .HasColumnName("PASSWORD")
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(d => d.Environment)
                .WithMany(e => e.Databases)
                .HasForeignKey(d => d.EnvironmentId)
                .IsRequired();
            
            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
            builder.AddEncryptionContextEntityProperties();
        }
    }
}