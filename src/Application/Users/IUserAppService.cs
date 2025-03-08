using Application.Users.Dtos;
using Domain.Result;
using Domain.Users;

namespace Application.Users;

public interface IUserAppService {
    Task<UserDto?> GetUser(int id);
    Task<List<UserDto>?> GetUsers();
    Task<IResult<UserDto>> CreateUser(CreateUserDto createUserDto); 
    Task<IResult> CreateToken(UserDto userDto);
    Task<IResult> ValidateUserEmailToken(int userId, string token);
    Task<IResult> SendEmailConfirmationToken(User user);
    Task<IResult> SendEmailConfirmationToken(int userId);
    Task<IResult> SendEmailConfirmationToken(string email);
}