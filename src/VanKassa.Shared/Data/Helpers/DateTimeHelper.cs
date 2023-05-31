namespace VanKassa.Shared.Data.Helpers;
public static class DateTimeHelper
{
    public static string ConvertDateTimeToShortDateQueryCorrectForm(DateTime dateTime)
        => dateTime.ToString("yyyy-MM-dd");
}
