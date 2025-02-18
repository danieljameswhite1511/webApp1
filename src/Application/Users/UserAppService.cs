using Application.Users.Dtos;
using Domain.Users;

namespace Application.Users;

public class UserAppService : IUserAppService {
    
    private readonly IUserDomainService _userDomainService;

    public UserAppService(IUserDomainService userDomainService) {
        _userDomainService = userDomainService;
    }

    public async Task<UserDto?> GetUser(int id) {
        var user = await _userDomainService.GetUserById(id);
        if (user == null) return null;
        return new UserDto {
            Name = user.Name
        };
    }
}