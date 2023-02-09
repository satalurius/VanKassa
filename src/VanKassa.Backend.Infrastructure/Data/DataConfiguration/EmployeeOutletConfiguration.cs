using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.DataConfiguration;

public class EmployeeOutletConfiguration : IEntityTypeConfiguration<EmployeeOutlet>
{
    public void Configure(EntityTypeBuilder<EmployeeOutlet> builder)
    {
        builder
            .ToTable("employee_outlet", Schemas.DboScheme);

        builder
            .HasKey(key => new { key.UserId, key.OutletId });

        builder
            .HasOne(p => p.Employee)
            .WithMany(p => p.UserOutlets)
            .HasForeignKey(fk => fk.OutletId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(p => p.Outlet)
            .WithMany(p => p.UserOutlets)
            .HasForeignKey(fk => fk.OutletId);
    }
}