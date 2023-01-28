namespace VanKassa.Domain.Dtos.Employees.Requests;

public class ChangedEmployeeRequestDto
{
    public int UserId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public IEnumerable<int> OutletsIds { get; set; } = Array.Empty<int>();
}