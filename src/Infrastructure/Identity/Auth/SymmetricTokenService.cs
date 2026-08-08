using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.auth.Services;
using Domain.Common.GlobalConfig;
using Domain.Users.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Auth;

public class SymmetricTokenService : ISymmetricTokenService
{
    private readonly SecurityKeys _securityKeys;
    private readonly Byte[] _secretKey;
    public SymmetricTokenService(IOptions<SecurityKeys> configuration)
    {
        _securityKeys = configuration.Value;
        _secretKey = System.Text.Encoding.UTF8.GetBytes(_securityKeys.SymmetricKey);
    } 
    
    public string GenerateToken(IUser user, int systemId, int? tenantId)
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimTypes.SystemId, systemId.ToString()),
        };

        if (tenantId.HasValue) {
            claims.Add(new Claim(CustomClaimTypes.TenantId, tenantId.Value.ToString()));
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor {
            
            Audience = "",
            Issuer = "",
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256),
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
