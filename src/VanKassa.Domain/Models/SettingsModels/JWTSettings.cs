namespace VanKassa.Domain.Models.SettingsModels;

public class JWTSettings
{
    public required string Secret { get; set; }
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public required int AccessTokenExpireSeconds { get; set; }
    public required int RefreshTokenExpireSeconds { get; set; }
}