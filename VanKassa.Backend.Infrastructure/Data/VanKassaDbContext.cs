using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Entities;
using User = VanKassa.Domain.Entities.User;

namespace VanKassa.Backend.Infrastructure.Data;

public class VanKassaDbContext : DbContext
{
    public VanKassaDbContext(DbContextOptions options) : base(options)
    { }

    #region DbSets

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Outlet> Outlets { get; set; } = null!;
    public DbSet<UserOutlet> UserOutlets { get; set; } = null!;

    public DbSet<UserCredentials> UsersCredentials { get; set; } = null!;
    
    #endregion

    public virtual DbSet<EmployeesDbDto> EmployeesDbDtos { get; set; } = null!;


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<EmployeesDbDto>().HasNoKey();
    }
}