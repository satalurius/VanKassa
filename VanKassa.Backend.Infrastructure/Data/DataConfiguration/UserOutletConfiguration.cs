using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class UserOutletConfiguration : IEntityTypeConfiguration<UserOutlet>
{
    public void Configure(EntityTypeBuilder<UserOutlet> builder)
    {
        builder
            .ToTable("user_outlet");

        builder
            .HasKey(key => new { key.UserId, key.OutletId });

        builder
            .HasOne(p => p.User)
            .WithMany(p => p.UserOutlets)
            .HasForeignKey(fk => fk.OutletId);

        builder
            .HasOne(p => p.Outlet)
            .WithMany(p => p.UserOutlets)
            .HasForeignKey(fk => fk.OutletId);
    }
}