using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VanKassa.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();

        return displayName ?? enumValue.ToString();
    }

    public static T? GetEnumByName<T>(string enumText)
        where T : struct, Enum
    {
        if (string.IsNullOrEmpty(enumText))
            return null;
                
        var enumValue = (T)Enum.Parse(typeof(T), enumText);

        return enumValue;
    }
}