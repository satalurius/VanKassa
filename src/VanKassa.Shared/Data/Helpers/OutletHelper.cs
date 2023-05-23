using VanKassa.Domain.Entities;

namespace VanKassa.Shared.Data.Helpers;
public static class OutletHelper
{
    public static string BuildOutletNameByAddresses(string city, string street, string? streetNumber)
            => string.Join(" ", city, street, streetNumber ?? string.Empty);
}
