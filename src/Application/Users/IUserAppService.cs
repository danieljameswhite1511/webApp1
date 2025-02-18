using Application.Users.Dtos;

namespace Application.Users;

public interface IUserAppService
{
    Task<UserDto?> GetUser(int id);
}