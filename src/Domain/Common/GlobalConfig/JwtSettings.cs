namespace Domain.Common.GlobalConfig;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string PrivateKeyBase64 { get; set; } = string.Empty;
}