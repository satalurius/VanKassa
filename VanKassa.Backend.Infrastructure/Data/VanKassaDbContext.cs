using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Infrastructure.IdentityEntities;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data;

public class VanKassaDbContext : IdentityDbContext<LoginUser, LoginRole, int>
{
    public VanKassaDbContext(DbContextOptions<VanKassaDbContext> options) : base(options)
    { }

    #region DbSets

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<Role> EmployeesRoles { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Outlet> Outlets { get; set; } = null!;
    public DbSet<EmployeeOutlet> EmployeeOutlets { get; set; } = null!;

    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    //public DbSet<UserCredentials> UsersCredentials { get; set; } = null!;
    
    #endregion

    public virtual DbSet<EmployeesDbDto> EmployeesDbDtos { get; set; } = null!;


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        MakeAllEntitiesToLowerCase(modelBuilder);

        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<EmployeesDbDto>().HasNoKey();
    }
 
    private void MakeAllEntitiesToLowerCase(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(MakeStringToSnakeCase(entity.GetTableName() ?? throw new ArgumentException("Entity table name is null")));

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(MakeStringToSnakeCase(property.GetColumnName()));        
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(MakeStringToSnakeCase(key.GetName() ?? throw new ArgumentException("Entity key name is null")));
            }

            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(MakeStringToSnakeCase(fk.GetConstraintName() ?? null));
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(MakeStringToSnakeCase(index.GetDatabaseName()));
            }
        } 
    }

    private string? MakeStringToSnakeCase(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        
        var startUnderscores = Regex.Match(text, @"^_+");
        return startUnderscores + Regex.Replace(text, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}