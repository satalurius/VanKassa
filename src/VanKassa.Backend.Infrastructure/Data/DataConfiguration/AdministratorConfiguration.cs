using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration
{
    internal class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder
                .ToTable("administrator", Schemas.DboScheme);

            builder
                .HasKey(key => key.UserId)
                .HasName("administrator_id");

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
                .Property(p => p.Phone)
                .HasColumnName("phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(p => p.UserName)
                .HasColumnName("user_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
