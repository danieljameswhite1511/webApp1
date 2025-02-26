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

    public async Task<List<User>?> GetUsers() {
        return await _userManager.GetUsersAsync();
    }
}