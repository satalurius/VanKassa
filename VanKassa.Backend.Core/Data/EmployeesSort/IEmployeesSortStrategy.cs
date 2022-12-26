using VanKassa.Domain.Dtos;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.EmployeesSort;

public interface IEmployeesSortStrategy
{
    IEnumerable<EmployeesDbDto> SortEmployees(IEnumerable<EmployeesDbDto> employees, SortDirection sortDirection);
}