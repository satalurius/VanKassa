
namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;

public class TopProductsDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IList<TopProductDto> TopProducts { get; set; } = new List<TopProductDto>();
}