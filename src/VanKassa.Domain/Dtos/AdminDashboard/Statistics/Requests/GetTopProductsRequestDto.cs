namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;

public class GetTopProductsRequestDto
{
    public int Positions { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
