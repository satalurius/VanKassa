namespace VanKassa.Domain.ViewModels;

public class PageEmployeesViewModel
{
    /// <summary>
    /// Количество сотрудников.
    /// </summary>
    public int TotalCount { get; set; }

    public IEnumerable<EmployeeViewModel> EmployeesViewModels { get; set; } = Array.Empty<EmployeeViewModel>();
}