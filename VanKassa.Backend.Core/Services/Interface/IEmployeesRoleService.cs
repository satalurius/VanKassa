using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeesRoleService
{
    Task<IEnumerable<EmployeesRoleDto>> GetAllRolesAsync();
}