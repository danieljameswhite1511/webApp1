using Application.Users.Dtos;

namespace Application.Users;

public interface IUserAppService {
    Task<UserDto?> GetUser(Guid id);
    Task<List<UserDto>?> GetUsers();
   
}