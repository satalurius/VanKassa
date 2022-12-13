namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу ролей пользователя.
/// </summary>
public class Role
{
    public required int RoleId { get; set; }
    public required string Name { get; set; }

    public required ICollection<User> Users { get; set; }
}