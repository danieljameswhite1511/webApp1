using Domain.Common.Result;
using Domain.Users.Entities;

namespace Domain.Users;

public interface IUserDomainService {
    Task<IResult<User>> CreateUserAsync(User user);
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>?> GetUsers();
    Task<IResult<User>> ConfirmEmailAsync(int userId, string token);
    Task<IResult> ValidatePasswordResetRequestAsync(string email, string code);
    Task<IResult> ResetPasswordAsync(string email, string token,string password);
    Task<IResult<string>> GenerateEmailConfirmationTokenAsync(int userId);
    Task<IResult<string>>  GeneratePasswordResetTokenAsync(string email);
    Task<IResult> SignInSpaAsync(string email, string password);
    Task<IResult<string>> SignInApiAsync(string email, string password);
}