using Domain.Result;

namespace Domain.Users;

public interface IUserDomainService
{
    Task<IResult<User>> CreateUserAsync(User user);
    Task<User?> GetUserById(int userId);
    Task<List<User>?> GetUsers();
}