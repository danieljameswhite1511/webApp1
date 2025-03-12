using Domain.Users;

namespace Domain.auth;

public interface ITokenService
{
    string GenerateToken(IUser user);
}