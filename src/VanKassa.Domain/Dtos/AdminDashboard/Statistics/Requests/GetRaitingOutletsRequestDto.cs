
namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
public class GetRaitingOutletsRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Positions { get; set; } = 5;
}
