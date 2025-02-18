using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Users;

public class AppUserManager : IUserManager<User, int>
{
    private readonly UserManager<AppUser> _userManager;

    public AppUserManager(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        if (user == null) return null;

        return new User
        {
            Name = user?.UserName,
        };
    }
}