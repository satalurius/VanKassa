namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class OrdersStatisticByPeriodDto
{
    public int Count { get; set; }
    public decimal TotalMoney { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}