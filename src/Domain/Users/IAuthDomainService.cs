using Domain.Common.Result;
using Domain.Users.Entities;

namespace Domain.Users;

public interface IAuthDomainService {
    Task<IResult<User>> CreateUserAsync(User user);
    Task<IResult<User>> ConfirmEmailAsync(Guid userId, string token);
    Task<IResult> ValidatePasswordResetRequestAsync(string email, string code);
    Task<IResult> ResetPasswordAsync(string email, string token,string password);
    Task<IResult<string>> GenerateEmailConfirmationTokenAsync(Guid userId);
    Task<IResult<string>>  GeneratePasswordResetTokenAsync(string email);
    Task<IResult> SignInSpaAsync(string email, string password, int systemId, int? tenantId);
    Task<IResult<string>> SignInApiAsync(string email, string password, int systemId, int? tenantId);
}