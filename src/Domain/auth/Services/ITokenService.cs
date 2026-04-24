using Domain.Users.Entities;

namespace Domain.auth.Services;

public interface ITokenService
{
    string GenerateToken(IUser user, int systemId, int? tenantId);
}