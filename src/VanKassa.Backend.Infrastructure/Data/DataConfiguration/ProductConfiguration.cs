using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("product", Schemas.DboScheme);

        builder
            .HasKey(key => key.ProductId)
            .HasName("product_id");

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(30)
            .IsRequired();
        
        builder
            .Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("DECIMAL")
            .HasPrecision(10)
            .IsRequired();

        builder
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(fk => fk.CategoryId);

    }
}