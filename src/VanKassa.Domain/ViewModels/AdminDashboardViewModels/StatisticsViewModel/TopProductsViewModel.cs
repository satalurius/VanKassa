using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;

namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;

public class TopProductsViewModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IList<TopProductViewModel> TopProducts { get; set; } = new List<TopProductViewModel>();
}
