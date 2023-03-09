namespace VanKassa.Domain.Dtos.Employees.Requests;

public class GeneratePdfEmployeesRequest
{
    public IList<string> Outlet { get; set; } = new List<string>();
    public IReadOnlyList<PdfEmployeeDto> EmployeesList { get; set; } = new List<PdfEmployeeDto>();
}