using Domain.Users.Entities;

namespace Domain.auth.Services;

public interface ISymmetricTokenService {
    string GenerateToken(IUser user, int systemId, int? tenantId);
}