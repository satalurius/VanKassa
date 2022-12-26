using VanKassa.Domain.Enums;

namespace VanKassa.Domain.Dtos;

public class EmployeesPageParameters
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public EmployeeTableColumn SortedColumn { get; set; }

    public SortDirection SortDirection { get; set; }

    public string FilterText { get; set; } = string.Empty;
}