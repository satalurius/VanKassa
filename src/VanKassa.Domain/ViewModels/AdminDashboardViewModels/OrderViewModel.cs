namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels;
public class OrderViewModel
{
    public Guid OrderId { get; set; }
    public DateTime Date { get; set; }
    public bool Canceled { get; set; }
    public decimal Price { get; set; }
    public bool ShowProductsInfo { get; set; }
    public OrderOutletViewModel? Outlet { get; set; }
    public IList<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
}
