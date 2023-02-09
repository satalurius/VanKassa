using Microsoft.AspNetCore.Identity;
using VanKassa.Backend.Core;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Backend.Infrastructure.IdentityEntities;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Models.SettingsModels;

namespace VanKassa.Backend.Api.Extensions;

public static class WebHostExtensions
{
    public static WebApplication SeedIdentity(this WebApplication host, IConfiguration configuration)
    {
        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;

        try
        {
            var userManager = services.GetRequiredService<UserManager<LoginUser>>();
            var roleManager = services.GetRequiredService<RoleManager<LoginRole>>();
            var superAdminSetting = configuration.GetSection(nameof(DefaultSuperAdminSettings))
                .Get<DefaultSuperAdminSettings>();

            if (superAdminSetting is null)
                throw new ArgumentNullException();
            
            var roleNames = new List<string>
            {
                Roles.Administrator, Roles.SuperAdministrator
            };

            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;

                if (!roleExist)
                {
                    roleManager.CreateAsync(new LoginRole
                    {
                        Name = roleName
                    }).Wait();
                }
            }

            var superAdmin = new LoginUser
            {
                UserName = superAdminSetting.UserName,
            };

            var password = superAdminSetting.Password;

            LoginUser? existedUser = userManager.FindByNameAsync(superAdmin.UserName).Result;

            if (existedUser is null)
            {
                var createSuperAdmin = userManager.CreateAsync(superAdmin, password).Result;

                if (createSuperAdmin.Succeeded)
                {
                    LoginUser? addedUser = userManager.FindByNameAsync(superAdmin.UserName).Result;

                    if (addedUser is not null)
                    {
                        userManager.AddToRoleAsync(addedUser, Roles.SuperAdministrator).Wait();
                    }
                }
            }
        }
        catch (Exception)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError("Error while creating identity user");
        }

        return host;
    }

    public static WebApplication SeedData(this WebApplication host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        try
        {
            using var context = services.GetRequiredService<VanKassaDbContext>();
            var configuration = services.GetRequiredService<IConfiguration>();

            var seeder = new DatabaseManager(context, configuration);
            seeder.SeedDatabase();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Произошла ошибка при заполнении базы данных");
        }

        return host;
    }
}