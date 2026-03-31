using Domain.Users.Entities;

namespace Domain.auth;

public interface ITokenService
{
    string GenerateToken(IUser user, int systemId, int? tenantId);
}