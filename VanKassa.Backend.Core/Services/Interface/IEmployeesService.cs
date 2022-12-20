using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeesService
{
    Task<IEnumerable<EmployeesDbDto>?> GetEmployeesAsync();
    Task<PageEmployeesDto?> GetEmployeesWithFiltersAsync(EmployeesPageParameters parameters);
}