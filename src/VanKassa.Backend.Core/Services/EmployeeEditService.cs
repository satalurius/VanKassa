using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services;

public class EmployeeEditService : IEmployeeEditService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IImageService imageService;

    public EmployeeEditService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper, IImageService imageService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.imageService = imageService;
    }

    /// <summary>
    /// Save new Employee
    /// </summary>
    /// <param name="savedEmployeeRequest"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task SaveEmployeeAsync(SavedEmployeeRequestDto savedEmployeeRequest)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            var role = dbContext.EmployeesRoles.First(dbRole => dbRole.RoleId == savedEmployeeRequest.RoleId);
            
            var user = new Employee
            {
                LastName = savedEmployeeRequest.LastName,
                FirstName = savedEmployeeRequest.FirstName,
                Patronymic = savedEmployeeRequest.Patronymic,
                Photo = imageService.SaveBase64ToImageFile(savedEmployeeRequest.Photo),
                Role = role,
                RoleId = role.RoleId
            };

            await dbContext.Employees.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var userOutlets = savedEmployeeRequest.OutletsIds
                .Select(outletId => new EmployeeOutlet
                {
                    UserId = user.UserId, OutletId = outletId
                })
                .ToList();

            await dbContext.EmployeeOutlets.AddRangeAsync(userOutlets);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (InvalidOperationException)
        {
            // TODO: Логировать
            throw new BadRequestException("Save was failed");
        }
        catch (ArgumentNullException)
        {
            // TODO: Логировать
            throw new BadRequestException("Save was failed");
        }
        catch (FormatException)
        {
            throw new BadRequestException("Save was failed");
        }
    }

    // TODO: Рефакторинг
    public async Task<EditedEmployeeDto> GetEditedEmployeeByIdAsync(int employeeId)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var usersOutlets = await dbContext.EmployeeOutlets
                .Join(dbContext.Employees, uo => uo.UserId,
                    u => u.UserId, (uo, u) =>
                        new
                        {
                            outletId = uo.OutletId,
                            userId = u.UserId,
                            lastName = u.LastName,
                            firstName = u.FirstName,
                            patronymic = u.Patronymic,
                            photo = u.Photo,
                            roleId = u.RoleId
                        })
                .Join(dbContext.Outlets, uo => uo.outletId,
                    o => o.OutletId,
                    (an, o) =>
                        new
                        {
                            uo = an,
                            outlet = o
                        })
                .Join(dbContext.EmployeesRoles, uo => uo.uo.roleId,
                    r => r.RoleId, (uo, r) =>
                        new
                        {
                            uo = uo,
                            role = r
                        })
                .Where(x => x.uo.uo.userId == employeeId)
                .ToListAsync();

            var userInformationForDto = usersOutlets.Select(sel => new
                {
                    userId = sel.uo.uo.userId,
                    FirstName = sel.uo.uo.firstName,
                    LastName = sel.uo.uo.lastName,
                    Patronymic = sel.uo.uo.patronymic,
                    Photo = sel.uo.uo.photo,
                    roleId = sel.role.RoleId,
                    role = new RoleDto
                    {
                        RoleId = sel.role.RoleId,
                        RoleName = sel.role.Name
                    },
                })
                .First();

            var editedEmployeeDto = new EditedEmployeeDto
            {
                UserId = userInformationForDto.userId,
                FirstName = userInformationForDto.FirstName,
                LastName = userInformationForDto.LastName,
                Patronymic = userInformationForDto.Patronymic,
                Photo = imageService.ConvertImageToBase64(userInformationForDto.Photo),
                Role = userInformationForDto.role
            };

            var outletsToAdd = new List<OutletDto>();

            usersOutlets.ForEach(us =>
            {
                outletsToAdd.Add(new OutletDto()
                {
                    Id = us.uo.outlet.OutletId,
                    City = us.uo.outlet.City,
                    Street = us.uo.outlet.Street,
                    StreetNumber = us.uo.outlet.StreetNumber ?? string.Empty
                });
            });

            editedEmployeeDto.Outlets = outletsToAdd;

            return editedEmployeeDto;
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException();
        }
        catch (ArgumentNullException)
        {
            throw new NotFoundException();
        }
    }

    public async Task ChangeEmployeeAsync(ChangedEmployeeRequestDto changedEmployeeRequest)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            var changedDbEmployee = await dbContext.Employees
                .FirstAsync(emp => emp.UserId == changedEmployeeRequest.UserId);

            changedDbEmployee.FirstName = changedEmployeeRequest.FirstName;
            changedDbEmployee.LastName = changedEmployeeRequest.LastName;
            changedDbEmployee.Patronymic = changedEmployeeRequest.Patronymic;
            changedDbEmployee.RoleId = changedEmployeeRequest.RoleId;
            changedDbEmployee.Photo = imageService.SaveBase64ToImageFile(changedEmployeeRequest.Photo);

            var changedOutletsForUser = await dbContext.EmployeeOutlets
                .Where(uo => uo.UserId == changedEmployeeRequest.UserId)
                .ToListAsync();

            dbContext.EmployeeOutlets.RemoveRange(changedOutletsForUser);

            var addedOutletsEntities = changedEmployeeRequest.OutletsIds.Select(outletId => new EmployeeOutlet
            {
                UserId = changedEmployeeRequest.UserId,
                OutletId = outletId
            });

            await dbContext.EmployeeOutlets.AddRangeAsync(addedOutletsEntities);

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (InvalidOperationException)
        {
            // TODO: логировать
            throw new BadRequestException("Update was failed.");
        }
        catch (ArgumentNullException)
        {
            // TODO: логировать
            throw new BadRequestException("Update was failed.");
        }
    }
}