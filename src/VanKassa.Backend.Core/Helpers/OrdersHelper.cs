using VanKassa.Domain.Enums;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Backend.Core.Helpers;

public static class OrdersHelper
{
    public static bool GetCanceledStatusFromOrderStatusEnum(OrderStatus orderStatus)
        => orderStatus == OrderStatus.Canceled;
}