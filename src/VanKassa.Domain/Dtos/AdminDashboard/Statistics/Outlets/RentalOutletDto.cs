namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;

public class RentalOutletDto
{
    public int OutletId { get; set; }
    public decimal TotalMoney { get; set; }
    public int CanceledCount { get; set; }
    public decimal CanceledMoney { get; set; }
    public decimal CouldEarn { get; set; }

    public string OutletName { get; set; }
}
