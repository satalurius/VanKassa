namespace VanKassa.Domain.Models.SettingsModels;

public class SqlScriptsSettings
{
    public string SeedDataFolder { get; init; } = string.Empty;

    public string RoleSeedFileName { get; init; } = string.Empty;
    public string UserSeedFileName { get; init; } = string.Empty;
    public string OutletSeedFileName { get; init; } = string.Empty;
    public string UserOutletSeedFileName { get; init; } = string.Empty;

    public string ModelToRolePathString()
        => $"{SeedDataFolder}{RoleSeedFileName}";

    public string ModelToUserPathString()
        => $"{SeedDataFolder}{UserSeedFileName}";

    public string ModelToOutletPathString()
        => $"{SeedDataFolder}{OutletSeedFileName}";

    public string ModelToOutletUserPathString()
        => $"{SeedDataFolder}{UserOutletSeedFileName}";
}