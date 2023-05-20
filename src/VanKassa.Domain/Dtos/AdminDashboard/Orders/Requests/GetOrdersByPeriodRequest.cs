namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;

public class GetOrdersByPeriodRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}