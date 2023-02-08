using VanKassa.Domain.Entities.Base;

namespace VanKassa.Domain.Entities;

public class UserCredentials : EntityBase
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int UserId { get; set; }
    public Employee Employee { get; set; } = null!;
}