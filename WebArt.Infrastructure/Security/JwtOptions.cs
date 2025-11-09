namespace WebArt.Infrastructure.Security;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int ExpiryMinutes { get; set; } = 60;
    public string? RoleClaim { get; set; } = "role";
}