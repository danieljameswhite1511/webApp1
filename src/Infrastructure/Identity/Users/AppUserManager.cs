using Domain.Result;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Users;

public class AppUserManager : IUserManager<User, int> {
    private readonly UserManager<AppUser> _userManager;

    public AppUserManager(UserManager<AppUser> userManager) {
        _userManager = userManager;
    }

    public async Task<IResult<User>> CreateUserAsync(User user){
        
        var appuser = new AppUser {
            UserName = user.Email,
            Email = user.Email,
        };
        
        var identityResult = await _userManager.CreateAsync(appuser, user.Password);
        if (identityResult.Succeeded) {
            user.Id = appuser.Id;
            return Result<User>.Success(user);
        }
        return Result<User>.Failed(identityResult.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<User?> GetUserByIdAsync(int id) {
        var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (appUser == null) return null;
        return new User {
            Id = appUser.Id,
            Email = appUser.Email,
            Name = appUser?.UserName,
        };
    }

    public async Task<User?> GetUserByEmailAsync(string email) {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null) return null;
        return new User {
            Id = appUser.Id,
            Name = appUser.UserName,
            Email = appUser.Email,
        };
    }

    public async Task<List<User>?> GetUsersAsync() {
        var users = await _userManager.Users.ToListAsync();
        return users.Select(x => new User {
            Id = x.Id,
            Name = x.UserName
        }).ToList();
    }

    public async Task<IResult<User>> ConfirmEmailAsync(int userId, string code) {
        var appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id.Equals(userId));
        if (appUser == null) return Result<User>.Failed("User not found");
        var result = await _userManager.ConfirmEmailAsync(appUser, code);
        if (result.Succeeded) {
            return Result<User>.Success(new User{Email = appUser.Email});
        }
        return Result<User>.Failed(result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<IResult<string>> GenerateEmailConfirmationTokenAsync(int userId) {
        var appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id.Equals(userId));
        if (appUser == null) return Result<string>.Failed("User not found");
        var token= await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        return Result<string>.Success(token);
    }
}