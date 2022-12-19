using System.ComponentModel.DataAnnotations.Schema;

namespace VanKassa.Domain.Dtos;

/// <summary>
/// Модель служит DTO для выгрузки данных, используя сложный запрос для получения данных для заполнения в таблице пользователей.
/// </summary>

[NotMapped]
public class EmployeesDbDto
{
    public required int UserId { get; set; }
    public required string Addresses { get; set; }
    public required string RoleName { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Patronymic { get; set; }
    public required string Photo { get; set; }
}