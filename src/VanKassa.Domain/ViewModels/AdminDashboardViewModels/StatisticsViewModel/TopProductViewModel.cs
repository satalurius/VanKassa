namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;

public class TopProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TotalMoney { get; set; }
}
