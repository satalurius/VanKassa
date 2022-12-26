namespace VanKassa.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role Role { get; set; } = new();

    public UserCredentials UserCredentials { get; set; } = new();
    
    public IEnumerable<UserOutlet> UserOutlets { get; set; } = new List<UserOutlet>();
}