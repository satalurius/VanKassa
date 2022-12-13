﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VanKassa.Domain.Entities;

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

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}