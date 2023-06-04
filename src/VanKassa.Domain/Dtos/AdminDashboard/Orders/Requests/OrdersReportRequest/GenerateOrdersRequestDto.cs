namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests.OrdersReportRequest;
public class GenerateOrdersRequestDto
{
    public IList<GenerateOrdersData> GenerateOrdersData { get; set; } = new List<GenerateOrdersData>();
}
