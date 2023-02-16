using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable("order", Schemas.DboScheme);

        builder
            .HasKey(key => key.OrderId)
            .HasName("order_id");

        builder
            .Property(p => p.Date)
            .HasColumnName("date")
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder
            .Property(p => p.Canceled)
            .HasColumnName("canceled")
            .HasColumnType("BOOLEAN")
            .IsRequired();
    }
}