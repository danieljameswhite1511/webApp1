using Domain.Users;

namespace Domain.auth;

public interface ITokenService
{
    string GenerateToken(User user);
}