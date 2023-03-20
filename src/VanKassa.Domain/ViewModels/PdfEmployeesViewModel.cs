namespace VanKassa.Domain.ViewModels;

public class PdfEmployeesViewModel
{
    public IReadOnlyList<string> Outlet { get; set; } = new List<string>();

    public IReadOnlyList<PdfEmployeeViewModel> EmployeeViewModels { get; set; } = new List<PdfEmployeeViewModel>();

}