using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Core.Utils;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Backend.Infrastructure.IdentityEntities;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.Models.SettingsModels;
using Role = VanKassa.Domain.Enums.Role;

namespace VanKassa.Backend.Core.Services
{
    public class AdministratorService : IAdministratorsService
    {

        private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
        private readonly IMapper mapper;
        private readonly IAuthenticationService authenticationService;
        private readonly UserManager<LoginUser> userManager;

        private readonly DefaultAdminSettings defaultAdminSettings;

        public AdministratorService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper, IAuthenticationService authenticationService,
            IOptions<DefaultAdminSettings> defaultAdminSettings, UserManager<LoginUser> userManager)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this.authenticationService = authenticationService;
            this.userManager = userManager;
            this.defaultAdminSettings = defaultAdminSettings.Value;
        }

        public async Task<IReadOnlyList<AdministratorDto>> GetAdministratorsAsync()
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var administrators = await dbContext.Administrators
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

                if (!administrators.Any())
                {
                    throw new NotFoundException();
                }

                return mapper.Map<IReadOnlyList<Administrator>, IReadOnlyList<AdministratorDto>>(administrators);
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
            catch (ArgumentNullException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
        }

        public async Task CreateAdministratorAsync(CreateAdministratorRequest createAdministratorRequest)
        {
            try
            {
                var userName =
                    EmployeeDataBuilder.BuildUserNameByFirstAndLastNames(createAdministratorRequest.FirstName,
                        createAdministratorRequest.LastName);

                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var dbAdmin = new Administrator
                {
                    LastName = createAdministratorRequest.LastName,
                    FirstName = createAdministratorRequest.FirstName,
                    Patronymic = createAdministratorRequest.Patronymic,
                    Phone = createAdministratorRequest.Phone,
                    UserName = userName
                };

                await dbContext.Administrators.AddAsync(dbAdmin);

                await dbContext.SaveChangesAsync();


                if (string.IsNullOrEmpty(createAdministratorRequest.Password))
                {
                    createAdministratorRequest.Password = defaultAdminSettings.Password;
                }

                await authenticationService.RegisterAsync(new RegisterDto
                {
                    UserName = userName,
                    Password = createAdministratorRequest.Password,
                    Role = Role.Administrator
                });
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
            catch (ArgumentNullException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
        }

        public async Task DeleteAdministratorsAsync(DeleteAdministratorsRequest deleteAdministratorRequest)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var deletedAdmins = await dbContext.Administrators
                    .Where(admin => deleteAdministratorRequest.DeletedIds.Contains(admin.UserId))
                    .ToListAsync();

                dbContext.Administrators.RemoveRange(deletedAdmins);

                await dbContext.SaveChangesAsync();
                
                foreach (var delAdmin in deletedAdmins)
                {
                    var deletedAdminIdentity = (await userManager.FindByNameAsync(delAdmin.UserName))!;

                    await userManager.DeleteAsync(deletedAdminIdentity);
                }
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
            catch (ArgumentNullException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
        }

        public async Task ChangeAdministratorAsync(ChangeAdministratorRequest changeAdministratorRequest)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var changedAdmin = await dbContext.Administrators
                    .FirstOrDefaultAsync(admin => admin.UserId == changeAdministratorRequest.AdminId);

                if (changedAdmin is null)
                {
                    throw new NotFoundException("Администратор для изменения не найден");
                }

                changedAdmin.LastName = changeAdministratorRequest.LastName;
                changedAdmin.FirstName = changeAdministratorRequest.FirstName;
                changedAdmin.Patronymic = changeAdministratorRequest.Patronymic;
                changedAdmin.Phone = changeAdministratorRequest.Phone;

                await dbContext.SaveChangesAsync();


                if (string.IsNullOrEmpty(changeAdministratorRequest.CurrentPassword) ||
                    string.IsNullOrEmpty(changeAdministratorRequest.NewPassword))
                {
                    return;
                }

                var changedAdminIdentity = (await userManager.FindByNameAsync(changedAdmin.UserName))!;

                await userManager.ChangePasswordAsync(changedAdminIdentity, changeAdministratorRequest.CurrentPassword,
                    changeAdministratorRequest.NewPassword);
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
            catch (ArgumentNullException)
            {
                throw new BadRequestException("Произошла ошибка получения администраторов");
            }
        }
    }
}
