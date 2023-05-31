using System.Text.Json.Serialization;
using VanKassa.Domain.Enums;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class OrdersPageParameters
{
    public int Page { get; set; }
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; } = 5;
    
    [JsonPropertyName("filter_text")]
    public string FilterText { get; set; } = string.Empty;
}