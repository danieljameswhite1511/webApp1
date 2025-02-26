using Application.Users.Dtos;
using Domain.Result;

namespace Application.Users;

public interface IUserAppService
{
    Task<UserDto?> GetUser(int id);
    Task<List<UserDto>?> GetUsers();
    Task<IResult<CreateUserDto>> CreateUser(CreateUserDto createUserDto); 
    Task<IResult<string>> CreateToken(UserDto userDto);
}