using Domain.Users;
using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserService<User, Guid> _userService;

    public UserDomainService(IUserService<User, Guid> userService) {
        _userService = userService;
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _userService.GetUserByEmailAsync(email);
    }

    public async Task<List<User>?> GetUsers()
    {
        return await _userService.GetUsersAsync();
    }

}