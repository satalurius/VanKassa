namespace VanKassa.Domain.Entities;

public class EmployeeOutlet
{
    public required int UserId { get; set; }
    public Employee Employee { get; set; }

    public required int OutletId { get; set; }
    public Outlet Outlet { get; set; }
}