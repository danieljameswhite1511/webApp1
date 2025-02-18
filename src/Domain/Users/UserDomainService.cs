namespace Domain.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserManager<User, int> _userManager;

    public UserDomainService(IUserManager<User, int> userManager) {
        _userManager = userManager;
    }

    public async Task<User?> GetUserById(int userId) {
        var user = await _userManager.GetUserByIdAsync(userId);
        if (user == null) return new User
        {
            Name = "Not Found",
        };
        return new User { Name = "hELLO" };
    }
}