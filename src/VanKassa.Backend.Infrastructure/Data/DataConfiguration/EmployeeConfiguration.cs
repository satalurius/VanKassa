using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("employee", Schemas.DboScheme);

        builder
            .HasKey(key => key.UserId)
            .HasName("employee_id");

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
            .Property(p => p.Fired)
            .HasColumnName("fired")
            .HasColumnType("BOOLEAN");

        builder
            .HasOne(p => p.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(fk => fk.RoleId);
    }
}