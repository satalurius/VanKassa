using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services
{
    public class AdministratorService : IAdministratorsService
    {

        private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
        private readonly IMapper mapper;


        public AdministratorService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
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
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();


                var dbAdmin = new Administrator
                {
                    LastName = createAdministratorRequest.LastName,
                    FirstName = createAdministratorRequest.FirstName,
                    Patronymic = createAdministratorRequest.Patronymic,
                    Phone = createAdministratorRequest.Phone
                };

                await dbContext.Administrators.AddAsync(dbAdmin);

                await dbContext.SaveChangesAsync();
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

                var deletedAdmins = dbContext.Administrators
                    .Where(admin => deleteAdministratorRequest.DeletedIds.Contains(admin.UserId));

                dbContext.Administrators.RemoveRange(deletedAdmins);

                await dbContext.SaveChangesAsync();
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
