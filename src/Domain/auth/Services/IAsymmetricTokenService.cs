using Domain.Users.Entities;

namespace Domain.auth.Services;

public interface IAsymmetricTokenService
{
    string GenerateToken(IUser user, int systemId, int? tenantId);
    string GetPublicKeyPem();
}