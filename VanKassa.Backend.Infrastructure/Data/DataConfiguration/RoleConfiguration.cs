using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .ToTable("role");

        builder
            .HasKey(key => key.RoleId)
            .HasName("role_id");

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(25)
            .IsRequired();
    }
}