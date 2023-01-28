namespace VanKassa.Domain.Entities;

public class UserOutlet
{
    public required int UserId { get; set; }
    public User User { get; set; }

    public required int OutletId { get; set; }
    public Outlet Outlet { get; set; }
}