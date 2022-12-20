using VanKassa.Backend.Core.Data.EmployeesSort.ConcreteStrategies;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.EmployeesSort;

/// <summary>
/// Служит для получения класса, реализующего сортировку по конкретному столбцу.
/// </summary>
public class SortEmployeesExecutor
{
    public IEmployeesSortStrategy GetSortImplementationByColumn(EmployeeTableColumn column)
        => column switch
        {
            EmployeeTableColumn.None => new EmptySortConcreteStrategy(),
            EmployeeTableColumn.LastName => new LastNameSortConcreteStrategy(),
            EmployeeTableColumn.FirstName => new FirstNameSortConcreteStrategy(),
            EmployeeTableColumn.Patronymic => new PatronymicConcreteStrategy(),
            EmployeeTableColumn.OutletAddresses => new AddressesSortConcreteStrategy(),
            EmployeeTableColumn.Role => new RoleSortConcreteStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(column), column, null)
        };
}