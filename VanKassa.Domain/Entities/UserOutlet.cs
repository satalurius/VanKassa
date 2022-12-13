namespace VanKassa.Domain.Entities;

public class UserOutlet
{
    public required int UserId { get; set; }
    public required User User { get; set; }

    public required int OutletId { get; set; }
    public required Outlet Outlet { get; set; }
}