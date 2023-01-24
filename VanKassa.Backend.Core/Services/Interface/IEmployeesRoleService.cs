using VanKassa.Domain.Dtos.Employees;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeesRoleService
{
    Task<IEnumerable<EmployeesRoleDto>> GetAllRolesAsync();
}