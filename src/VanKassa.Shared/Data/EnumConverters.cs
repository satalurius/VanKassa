using VanKassa.Domain.Constants;
using VanKassa.Domain.Enums;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Shared.Data
{
    public static class EnumConverters
    {
        public static string ConvertRoleEnumToConstantValue(Role role)
            => role switch
            {
                Role.SuperAdministrator => Roles.SuperAdministrator,
                Role.Administrator => Roles.Administrator,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };

        public static string ConvertMonthEnumToRussianMonth(Month month)
        {
            var russianMonth = new Dictionary<int, string>
            {
                [1] = "Январь",
                [2] = "Февраль",
                [3] = "Март",
                [4] = "Апрель",
                [5] = "Май",
                [6] = "Июнь",
                [7] = "Июль",
                [8] = "Август",
                [9] = "Сентябрь",
                [10] = "Октябрь",
                [11] = "Ноябрь",
                [12] = "Декабрь",
            };

            return russianMonth[(int)month];
        }
    }
}
