namespace ECommerce.Application.Models.VMs;

public class JwtTokenSettings
{
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int RefreshTokenTTL { get; set; }
    public int AccessTokenExpirationMinutes { get; set; }
}
