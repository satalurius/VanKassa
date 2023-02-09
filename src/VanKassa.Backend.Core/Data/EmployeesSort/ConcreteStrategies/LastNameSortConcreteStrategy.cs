using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.EmployeesSort.ConcreteStrategies;

public class LastNameSortConcreteStrategy : IEmployeesSortStrategy
{
    public IEnumerable<EmployeesDbDto> SortEmployees(IEnumerable<EmployeesDbDto> employees, SortDirection sortDirection)
        => sortDirection switch
        {
            SortDirection.None => employees.OrderBy(emp => emp.UserId),
            SortDirection.Ascending => employees.OrderBy(emp => emp.LastName),
            SortDirection.Descending => employees.OrderByDescending(emp => emp.LastName),
            _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
        };
}