using Domain.Result;

namespace Domain.Users;

public interface IUserDomainService {
    Task<IResult<User>> CreateUserAsync(User user);
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>?> GetUsers();
    Task<IResult<User>> ConfirmEmailAsync(int userId, string code);
    Task<IResult<string>> GenerateEmailConfirmationTokenAsync(int userId);
}