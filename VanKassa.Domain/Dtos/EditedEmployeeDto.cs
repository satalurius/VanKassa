namespace VanKassa.Domain.Dtos;

public class EditedEmployeeDto
{
    public int UserId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public IEnumerable<RoleDto> Roles { get; set; } = Array.Empty<RoleDto>();
    public IEnumerable<OutletDto> Outlets { get; set; } = new List<OutletDto>();
}