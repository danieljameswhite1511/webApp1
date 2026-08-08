/* generate pemPrivateKey
     * openssl genpkey --algorithm RSA -out file.txt
     * [System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes("$PWD\file.txt"))
     * or
     * openssl genrsa -out private_key.pem 2048
     * then
     * $pemText = Get-Content .\private_key.pem -Raw
     * [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($pemText))*/

using System.Security.Cryptography;
using Domain.auth.Services;
using Domain.Common.GlobalConfig;
using Domain.Users.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Auth;

public class AsymmetricTokenService : IAsymmetricTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly RsaSecurityKey _privateKey;
    private readonly JsonWebTokenHandler _tokenHandler = new();

    public AsymmetricTokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
        // Decode the Base64 string back to the full PEM text block
        var pemString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_jwtSettings.PrivateKeyBase64));
        var rsa = RSA.Create();
        rsa.ImportFromPem(pemString);
        _privateKey = new RsaSecurityKey(rsa);
    }

    public string GenerateToken(IUser user, int systemId, int? tenantId)
    {
        var claims = new Dictionary<string, object> {
            [JwtRegisteredClaimNames.Sub] = user.Id,
            [JwtRegisteredClaimNames.Email] = user.Email,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
            ["system_id"] = systemId.ToString()
        };

        if (tenantId.HasValue) {
            claims["tenant_id"] = tenantId.Value.ToString();
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Claims = claims,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256)
        };

        return _tokenHandler.CreateToken(tokenDescriptor);
    }

    public string GetPublicKeyPem()
    {
        using var rsa = _privateKey.Rsa ?? RSA.Create(_privateKey.Parameters);
        return rsa.ExportRSAPublicKeyPem();
    }
}

/*using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.auth.Services;
using Domain.Users.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Auth;

public class AsymmetricTokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly Byte[] _secretKey;

    
     #1#
    public AsymmetricTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = System.Text.Encoding.UTF8.GetBytes(_configuration["PrivateKeyBase64"]);
        
        var base64Key = _configuration["JwtSettings:PrivateKeyBase64"];
        var pemString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Key));

        var rsa = RSA.Create();
        rsa.ImportFromPem(pemString);
        
    }

    public string GenerateToken(IUser user, int systemId, int? tenantId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimTypes.SystemId, systemId.ToString()),
        };

        if (tenantId.HasValue)
        {
            claims.Add(new Claim(CustomClaimTypes.TenantId, tenantId.Value.ToString()));
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {

            Audience = "",
            Issuer = "",
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}*/