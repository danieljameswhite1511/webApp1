using Domain.Common.Entities;
using Domain.Common.Result;
using Domain.Users.Entities;

namespace Domain.Users;

public interface  IUserManager<TUser, TPrimaryKey> where TUser : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey> {
    Task<IResult<TUser>> CreateUserAsync(TUser user);
    Task<TUser?> GetUserByIdAsync(TPrimaryKey id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<TUser>?> GetUsersAsync();
    Task<IResult<TUser>> ConfirmEmailAsync(TPrimaryKey userId, string code);
    Task<IResult<string>> GenerateEmailConfirmationTokenAsync(TPrimaryKey userId);
    Task<IResult> SignInSpaAsync(string email, string password);
    Task<IResult<string>> SignInApiAsync(string email, string password);
    Task<IResult<string>>  GeneratePasswordResetTokenAsync(string email);
    Task<IResult> ValidatePasswordResetRequestAsync(string email, string token);
    Task<IResult> ResetPasswordAsync(string email, string token, string password);
}