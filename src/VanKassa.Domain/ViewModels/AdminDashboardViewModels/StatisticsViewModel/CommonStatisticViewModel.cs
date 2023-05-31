namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;

public class CommonStatisticViewModel
{
    public string MoneysForMonth { get; set; }
    public string MoneyForToday { get; set; }
    public string OrdersCountForToday { get; set; }

    public CommonStatisticViewModel(decimal moneyForMonth, decimal moneyForToday, int ordersCountForToday)
    {
        MoneysForMonth = $"{moneyForMonth}₽";
        MoneyForToday = $"{moneyForToday}₽";
        OrdersCountForToday = ordersCountForToday.ToString();
    }
}