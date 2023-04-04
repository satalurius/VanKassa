using VanKassa.Domain.Constants;
using VanKassa.Domain.Enums;

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
    }
}
