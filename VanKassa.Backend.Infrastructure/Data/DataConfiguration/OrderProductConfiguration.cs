using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder
            .ToTable("order_product");
        
        builder
            .HasKey(key => new { key.OrderId, key.ProductId });

        builder
            .HasOne(p => p.Order)
            .WithMany(s => s.OrderProducts)
            .HasForeignKey(fk => fk.OrderId);

        builder
            .HasOne(p => p.Product)
            .WithMany(s => s.OrderProducts)
            .HasForeignKey(fk => fk.ProductId);
    }
}