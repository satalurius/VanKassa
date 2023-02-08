namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу ролей пользователя.
/// </summary>
public class Role
{
    public  int RoleId { get; set; }
    public  string Name { get; set; }

    public  ICollection<Employee> Users { get; set; }
}