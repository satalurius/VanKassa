namespace VanKassa.Domain.Dtos;

public class OutletDto
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
}