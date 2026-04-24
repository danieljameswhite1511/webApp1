using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.auth;
using Domain.auth.Services;
using Domain.Users.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly Byte[] _secretKey;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = System.Text.Encoding.UTF8.GetBytes(_configuration["SecretKey"]);
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