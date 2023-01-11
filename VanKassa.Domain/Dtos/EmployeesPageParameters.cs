using VanKassa.Domain.Enums;

namespace VanKassa.Domain.Dtos;

public class EmployeesPageParameters
{
    public int Page { get; set; } = 0;

    public int PageSize { get; set; } = 5;

    public EmployeeTableColumn SortedColumn { get; set; }

    public SortDirection SortDirection { get; set; }

    public string FilterText { get; set; } = string.Empty;
}