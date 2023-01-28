using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("category");
        
        builder
            .HasKey(key => key.CategoryId)
            .HasName("category_id");
        
        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(25)
            .IsRequired();
    }
}