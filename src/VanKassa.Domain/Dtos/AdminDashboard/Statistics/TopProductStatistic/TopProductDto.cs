namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;

public class TopProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TotalMoney { get; set; }
}
