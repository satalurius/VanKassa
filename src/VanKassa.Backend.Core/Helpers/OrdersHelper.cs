using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Helpers;

public static class OrdersHelper
{
    public static bool GetCanceledStatusFromOrderStatusEnum(OrderStatus orderStatus)
        => orderStatus == OrderStatus.Canceled;
}