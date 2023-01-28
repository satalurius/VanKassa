using VanKassa.Domain.Enums;

namespace VanKassa.Presentation.BlazorWeb.Extensions;

public static class CustomWebExtensionsMappers
{
    public static SortDirection MapMudSortDirectionToDirection(MudBlazor.SortDirection mudSortDirection)
        => mudSortDirection switch
        {
            MudBlazor.SortDirection.None => SortDirection.None,
            MudBlazor.SortDirection.Ascending => SortDirection.Ascending,
            MudBlazor.SortDirection.Descending => SortDirection.Descending,
            _ => throw new ArgumentOutOfRangeException(nameof(mudSortDirection), mudSortDirection, null)
        };
}