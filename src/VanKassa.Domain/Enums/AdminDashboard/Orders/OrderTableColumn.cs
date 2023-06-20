
using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.Enums.AdminDashboard.Orders;
public enum OrderTableColumn
{
    None = 0,
    [Display(Name = "Идентификатор")]
    OrderId = 1,
    [Display(Name = "Дата")]
    Date = 2,
    [Display(Name = "Статус")]
    Status = 3,
    [Display(Name = "Цена")]
    Price = 4,
    [Display(Name = "Филиал")]
    OutletName = 5,
}
