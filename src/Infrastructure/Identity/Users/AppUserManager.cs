using Domain.Result;
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
        ;
        
    }

    public async Task<IResult<User>> CreateUserAsync(User user)
    {
        
        var appuser = new AppUser {
            Id = user.Id,
            UserName = user.Email,
            Email = user.Email,
        };
        
        var identityResult = await _userManager.CreateAsync(appuser, user.Password);
        
        if (identityResult.Succeeded) {
            return Result<User>.Success(user);
        }
        
        return Result<User>.Failed(identityResult.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        if (appUser == null) return null;

        return new User
        {
            Name = appUser?.UserName,
        };
    }

    public async Task<List<User>?> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users.Select(x => new User
        {
            Name = x.UserName

        }).ToList();

    }
}