namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;
public class OrdersReportDto
{
    public required byte[] Content { get; set; }
    public required string ContentType { get; set; }
    public required string FileName { get; set; }
}
