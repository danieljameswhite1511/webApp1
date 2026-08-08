using Domain.Common.Result;
using Domain.Users.Entities;

namespace Domain.Users;

public class AuthDomainService : IAuthDomainService {
    
    private readonly IUserManager<User, Guid> _userManager;
    public AuthDomainService(IUserManager<User, Guid> userManager) {
        _userManager = userManager;
    }
    public async Task<IResult<User>> CreateUserAsync(User user) {
        var result = await _userManager.CreateUserAsync(user);
        return result;
    }
  
    public Task<IResult<User>> ConfirmEmailAsync(Guid userId, string code) {
        return _userManager.ConfirmEmailAsync(userId, code);
    }
    
    public Task<IResult> ValidatePasswordResetRequestAsync(string email, string code) {
        return _userManager.ValidatePasswordResetRequestAsync(email, code);
    }

    public async Task<IResult> ResetPasswordAsync(string email, string token, string password)
    {
        return await _userManager.ResetPasswordAsync(email, token, password);
    }

    public async Task<IResult<string>> GenerateEmailConfirmationTokenAsync(Guid userId) {
        return await _userManager.GenerateEmailConfirmationTokenAsync(userId);
    }

    public async Task<IResult<string>> GeneratePasswordResetTokenAsync(string email) {
        return await _userManager.GeneratePasswordResetTokenAsync(email);
    }

    public async Task<IResult> SignInSpaAsync(string email, string password, int systemId, int? tenantId) {
        var result = await _userManager.SignInSpaAsync(email, password, systemId, tenantId);
        return result;
    }

    public async Task<IResult<string>> SignInApiAsync(string email, string password,  int systemId, int? tenantId) {
        return await _userManager.SignInApiAsync(email, password,  systemId, tenantId);
    }
}