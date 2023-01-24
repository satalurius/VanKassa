using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.EmployeesSort.ConcreteStrategies;

public class FirstNameSortConcreteStrategy : IEmployeesSortStrategy
{
    public IEnumerable<EmployeesDbDto> SortEmployees(IEnumerable<EmployeesDbDto> employees, SortDirection sortDirection)
        => sortDirection switch
        {
            SortDirection.None => employees.OrderBy(emp => emp.UserId),
            SortDirection.Ascending => employees.OrderBy(emp => emp.FirstName),
            SortDirection.Descending => employees.OrderByDescending(emp => emp.FirstName),
            _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
        };
}