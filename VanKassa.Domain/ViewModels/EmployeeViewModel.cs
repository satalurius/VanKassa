namespace VanKassa.Domain.ViewModels;

public class EmployeeViewModel
{
    public required int Id { get; set; }
    public required string LastName { get; init; }
    public required string FirstName { get; init; }
    public required string Patronymic { get; init; }
    public required string Addresses { get; init; }
    public required string Role { get; init; }
    public required string Photo { get; init; }
}