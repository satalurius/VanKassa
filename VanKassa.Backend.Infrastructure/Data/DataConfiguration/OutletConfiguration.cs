using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class OutletConfiguration : IEntityTypeConfiguration<Outlet>
{
    public void Configure(EntityTypeBuilder<Outlet> builder)
    {
        builder
            .ToTable("outlet");

        builder
            .HasKey(key => key.OutletId)
            .HasName("outlet_id");

        builder
            .Property(p => p.City)
            .HasColumnName("city")
            .HasColumnType("VARCHAR(25)")
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(p => p.Street)
            .HasColumnName("street")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(p => p.StreetNumber)
            .HasColumnName("street_number")
            .HasColumnType("VARCHAR")
            .HasMaxLength(15);
    }
}