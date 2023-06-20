namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;
public class RaitingOutletDto
{
    public int OutletId { get; set; }
    public int Raiting { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Percent { get; set; }
}
