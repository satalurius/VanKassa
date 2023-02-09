namespace VanKassa.Domain.Models.SettingsModels;

public class SqlScriptsSettings
{
    public string SeedDataFolder { get; init; } = string.Empty;
    public string RoleSeedFileName { get; init; } = string.Empty;
    public string UserSeedFileName { get; init; } = string.Empty;
    public string OutletSeedFileName { get; init; } = string.Empty;
    public string UserOutletSeedFileName { get; init; } = string.Empty;
    public string UserCredentialsSeedFileName { get; init; } = string.Empty;

    public string ModelToRolePathString()
        => Path.Combine(SeedDataFolder, RoleSeedFileName);

    public string ModelToUserPathString()
        => Path.Combine(SeedDataFolder, UserSeedFileName);

    public string ModelToOutletPathString()
        => Path.Combine(SeedDataFolder, OutletSeedFileName);

    public string ModelToOutletUserPathString()
        => Path.Combine(SeedDataFolder, UserOutletSeedFileName);

    public string ModelToUserCredentialsPathString()
        => Path.Combine(SeedDataFolder, UserCredentialsSeedFileName);
}