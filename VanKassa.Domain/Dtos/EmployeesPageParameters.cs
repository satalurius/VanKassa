using System.Text.Json.Serialization;
using VanKassa.Domain.Enums;

namespace VanKassa.Domain.Dtos;

public class EmployeesPageParameters
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; } = 5;
    
    [JsonPropertyName("sorted_column")]
    public EmployeeTableColumn SortedColumn { get; set; }

    [JsonPropertyName("sort_direction")]
    public SortDirection SortDirection { get; set; }

    [JsonPropertyName("filter_text")]
    public string FilterText { get; set; } = string.Empty;
}