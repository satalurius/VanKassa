using VanKassa.Domain.Dtos.AdminDashboard.Orders;

namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels;
public class TableOrderViewModel
{
    public int TotalCount { get; set; }
    public IList<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
}
