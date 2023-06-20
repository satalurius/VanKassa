namespace VanKassa.Domain.Constants;

public static class Roles
{
    public const string SuperAdministrator = "SuperAdministrator";
    public const string Administrator = "Administrator";

    public const string SuperAndAdministratorRoles = $"{SuperAdministrator}, {Administrator}";
}