using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Services;

public class EmployeeEditService : IEmployeeEditService
{
    private readonly VanKassaDbContext _dbContext;

    public EmployeeEditService(VanKassaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveEmployee(EditedEmployeeDto editedEmployee)
    {
        try
        {
            var role = _dbContext.Roles.First(dbRole => dbRole.RoleId == editedEmployee.Roles.First().RoleId);
            var user = new User
            {
                LastName = editedEmployee.LastName,
                FirstName = editedEmployee.FirstName,
                Patronymic = editedEmployee.Patronymic,
                Photo = editedEmployee.Photo,
                Role = role,
                RoleId = role.RoleId
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var userOutlets = new List<UserOutlet>();

            foreach (var outlet in editedEmployee.Outlets)
            {
                userOutlets.Add(new UserOutlet
                {
                    UserId = user.UserId,
                    OutletId = outlet.Id
                });
            }
            
            await _dbContext.UserOutlets.AddRangeAsync(userOutlets);
            await _dbContext.SaveChangesAsync();
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
}