using Domain.Result;

namespace Domain.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserManager<User, int> _userManager;
    public UserDomainService(IUserManager<User, int> userManager) {
        _userManager = userManager;
    }
    public async Task<IResult<User>> CreateUserAsync(User user) {
        var result = await _userManager.CreateUserAsync(user);
        
        return result;
    }
    public async Task<User?> GetUserById(int userId) {
        var user = await _userManager.GetUserByIdAsync(userId);
        return user;
    }

    public async Task<User?> GetUserByEmail(string email) {
        return await _userManager.GetUserByEmailAsync(email);
    }
    
    public async Task<List<User>?> GetUsers() {
        return await _userManager.GetUsersAsync();
    }
    public Task<IResult<User>> ConfirmEmailAsync(int userId, string code) {
        return _userManager.ConfirmEmailAsync(userId, code);
    }

    public async Task<IResult<string>> GenerateEmailConfirmationTokenAsync(int userId) {
        return await _userManager.GenerateEmailConfirmationTokenAsync(userId);
    }

    public async Task<IResult> SignInSpaAsync(string email, string password)
    {
        var result = await _userManager.SignInSpaAsync(email, password);
        return result;
    }

    public async Task<IResult<string>> SignInApiAsync(string email, string password)
    {
        return await _userManager.SignInApiAsync(email, password);
    }
}