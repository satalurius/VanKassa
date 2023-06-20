using VanKassa.Domain.Entities.Base;

namespace VanKassa.Domain.Entities;

public class Employee : EmployeeBase
{
    public string Photo { get; set; } = string.Empty;
    public bool Fired { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; } = new();

    public UserCredentials UserCredentials { get; set; } = null!;
    
    public IEnumerable<EmployeeOutlet> UserOutlets { get; set; } = new List<EmployeeOutlet>();
}