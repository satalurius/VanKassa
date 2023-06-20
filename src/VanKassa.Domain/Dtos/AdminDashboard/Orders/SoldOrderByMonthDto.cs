using System.Text.Json.Serialization;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class SoldOrderByMonthDto
{
    public Month Month { get; set; }
    public int Year { get; set; }
    public int Count { get; set; }
    public decimal TotalMoney { get; set; }
}