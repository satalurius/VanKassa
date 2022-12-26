using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
{
    public void Configure(EntityTypeBuilder<UserCredentials> builder)
    {
        builder
            .ToTable("user_credentials");

        builder
            .HasKey(key => key.Id)
            .HasName("id");
        
        builder
            .Property(p => p.UserName)
            .HasColumnName("user_name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        
        builder
            .Property(p => p.Password)
            .HasColumnName("password")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasOne(p => p.User)
            .WithOne(p => p.UserCredentials)
            .HasForeignKey<UserCredentials>(fk => fk.UserId);
    }
}