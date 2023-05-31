namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;

public class SoldOrderByMonthViewModel
{
    public string Month { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Count { get; set; }
    public decimal TotalMoney { get; set; }
}
