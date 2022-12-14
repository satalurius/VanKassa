using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("user");

        builder
            .HasKey(key => key.UserId)
            .HasName("user_id");

        builder
            .Property(p => p.LastName)
            .HasColumnName("last_name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(p => p.FirstName)
            .HasColumnName("fist_name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(p => p.Patronymic)
            .HasColumnName("patronymic")
            .HasColumnType("VARCHAR")
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(p => p.Photo)
            .HasColumnName("photo")
            .HasColumnType("TEXT");

        builder
            .HasOne(p => p.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(fk => fk.RoleId);
    }
}