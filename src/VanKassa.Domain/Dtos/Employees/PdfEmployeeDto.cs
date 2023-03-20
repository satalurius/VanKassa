namespace VanKassa.Domain.Dtos.Employees;

public class PdfEmployeeDto
{
    public required string Addresses { get; set; } = string.Empty;
    public required string RoleName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required string FirstName { get; set; } = string.Empty;
    public required string Patronymic { get; set; } = string.Empty;
}