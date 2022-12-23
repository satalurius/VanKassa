namespace VanKassa.Domain.Dtos;

public class EditedEmployeeDto
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public IEnumerable<RoleDto> Roles { get; set; } = Array.Empty<RoleDto>();
    public IEnumerable<OutletDto> Outlets { get; set; } = Array.Empty<OutletDto>();
}