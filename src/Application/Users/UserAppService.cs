using Application.Users.Dtos;
using Domain.auth;
using Domain.Result;
using Domain.Users;

namespace Application.Users;

public class UserAppService : IUserAppService {
    
    private readonly IUserDomainService _userDomainService;
    private readonly ITokenService _tokenService;

    public UserAppService(IUserDomainService userDomainService
        , ITokenService tokenService)
    {
        _userDomainService = userDomainService;
        _tokenService = tokenService;
    }

    public async Task<UserDto?> GetUser(int id) {
        var user = await _userDomainService.GetUserById(id);
        if (user == null) return null;
        return new UserDto {
            Name = user.Name
        };
    }

    public async Task<List<UserDto>?> GetUsers()
    {
        var users = await _userDomainService.GetUsers();

        return users?.Select(x => new UserDto {
            Name = x.Name
        }).ToList();
    }

    public async Task<IResult<CreateUserDto>> CreateUser(CreateUserDto createUserDto) {
        var user = new User {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
        };

        var userCreateResult = await _userDomainService.CreateUserAsync(user);

        if (!userCreateResult.Succeeded) 
            return Result<CreateUserDto>.Failed(userCreateResult.Errors);
        
        var token = _tokenService.GenerateToken(userCreateResult.Value);
        createUserDto.Token = token;
        return Result<CreateUserDto>.Success(createUserDto);

    }

    public async Task<IResult<string>> CreateToken(UserDto userDto)
    {
        var user = await _userDomainService.GetUserById(userDto.Id);
        if (user == null) return Result<string>.Failed("User not found");
        
        return Result<string>.Success(_tokenService.GenerateToken(user));

    }
}