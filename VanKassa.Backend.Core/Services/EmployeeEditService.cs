using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Services;

public class EmployeeEditService : IEmployeeEditService
{
    private readonly IDbContextFactory<VanKassaDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public EmployeeEditService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task SaveEmployeeAsync(EditedEmployeeDto editedEmployee)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var role = dbContext.Roles.First(dbRole => dbRole.RoleId == editedEmployee.Roles.First().RoleId);
            var user = new User
            {
                LastName = editedEmployee.LastName,
                FirstName = editedEmployee.FirstName,
                Patronymic = editedEmployee.Patronymic,
                Photo = editedEmployee.Photo,
                Role = role,
                RoleId = role.RoleId
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var userOutlets = new List<UserOutlet>();

            foreach (var outlet in editedEmployee.Outlets)
            {
                userOutlets.Add(new UserOutlet
                {
                    UserId = user.UserId,
                    OutletId = outlet.Id
                });
            }

            await dbContext.UserOutlets.AddRangeAsync(userOutlets);
            await dbContext.SaveChangesAsync();
        }
        catch (InvalidOperationException ex)
        {
            // TODO: Логировать
            throw new InvalidOperationException();
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Логировать
            throw new InvalidOperationException();
        }
    }

    // TODO: Рефакторинг
    public async Task<EditedEmployeeDto> GetEditedEmployeeById(int employeeId)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var usersOutlets = await dbContext.UserOutlets
                .Join(dbContext.Users, uo => uo.UserId,
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
                .Join(dbContext.Roles, uo => uo.uo.roleId,
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
                    role = new List<RoleDto>
                    {
                        new()
                        {
                            RoleId = sel.role.RoleId,
                            RoleName = sel.role.Name
                        }
                    },
                })
                .First();

            var editedEmployeeDto = new EditedEmployeeDto
            {
                UserId = userInformationForDto.userId,
                FirstName = userInformationForDto.FirstName,
                LastName = userInformationForDto.LastName,
                Patronymic = userInformationForDto.Patronymic,
                Photo = userInformationForDto.Photo,
                Roles = userInformationForDto.role
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
            return new EditedEmployeeDto();
        }
        catch (ArgumentNullException)
        {
            return new EditedEmployeeDto();
        }
    }

    public async Task ChangeExistEmployeeAsync(EditedEmployeeDto changedEmployee)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var changedDbEmployee = await dbContext.Users
                .FirstOrDefaultAsync(emp => emp.UserId == changedEmployee.UserId);

            changedDbEmployee.FirstName = changedEmployee.FirstName;
            changedDbEmployee.LastName = changedEmployee.LastName;
            changedDbEmployee.Patronymic = changedEmployee.Patronymic;
            changedDbEmployee.RoleId = changedEmployee.Roles.First().RoleId;
            changedDbEmployee.Photo = changedEmployee.Photo;

            var changedOutletsForUser = await dbContext.UserOutlets
                .Where(uo => uo.UserId == changedEmployee.UserId)
                .ToListAsync();
            
            dbContext.UserOutlets.RemoveRange(changedOutletsForUser);

            var addedOutletsEntities = changedEmployee.Outlets.Select(chO => new UserOutlet
            {
                UserId = changedEmployee.UserId,
                OutletId = chO.Id
            });

            dbContext.UserOutlets.AddRangeAsync(addedOutletsEntities);

            await dbContext.SaveChangesAsync();
        }
        catch (InvalidOperationException)
        {
            // TODO: логировать
        }
        catch (ArgumentNullException)
        {
            // TODO: логировать
        }
    }
}