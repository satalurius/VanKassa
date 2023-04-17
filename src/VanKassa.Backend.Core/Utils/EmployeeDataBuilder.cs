namespace VanKassa.Backend.Core.Utils
{
    internal static class EmployeeDataBuilder
    {
        public static string BuildUserNameByFirstAndLastNames(string firstName, string lastName)
        {
            firstName = TranslateUtil.TransplitRussianToEnglish(firstName);
            lastName = TranslateUtil.TransplitRussianToEnglish(lastName);

            return string.Concat(firstName[0].ToString(), firstName.AsSpan(1, 2))
                           + string.Concat(lastName[0].ToString(), lastName.AsSpan(1, 2));
        }
    }
}
