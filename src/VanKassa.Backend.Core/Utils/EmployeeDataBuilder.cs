namespace VanKassa.Backend.Core.Utils
{
    internal static class EmployeeDataBuilder
    {
        public static string BuildUserNameByFirstAndLastNames(string firstName, string lastName)
           => string.Concat(firstName[0].ToString(), firstName.AsSpan(1, 2))
                           + string.Concat(lastName[0].ToString(), lastName.AsSpan(1, 2));
    }
}
