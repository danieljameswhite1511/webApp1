using Application.Users.Dtos;
using Domain.auth;
using Domain.Notifications;
using Domain.Result;
using Domain.Users;
using Infrastructure.Urls;

namespace Application.Users;

public class UserAppService : IUserAppService {
    
    private readonly IUserDomainService _userDomainService;
    private readonly ITokenService _tokenService;
    private readonly INotification _notification;
    private readonly IUriBuilderService _uriBuilderService;

    public UserAppService(IUserDomainService userDomainService
        , ITokenService tokenService
        , INotification notification
        , IUriBuilderService uriBuilderService)
    {
        _userDomainService = userDomainService;
        _tokenService = tokenService;
        _notification = notification;
        _uriBuilderService = uriBuilderService;
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
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public async Task<IResult<UserDto>> CreateUser(CreateUserDto createUserDto) {
        var user = new User {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
        };

        var userCreateResult = await _userDomainService.CreateUserAsync(user);
        if (!userCreateResult.Succeeded) 
            return Result<UserDto>.Failed(userCreateResult.Errors);

        user = userCreateResult.Value;
       
        var emailConfirmationResult = await SendEmailConfirmationToken(user);
        
        if (!emailConfirmationResult.Succeeded) {
            return Result<UserDto>.Failed(emailConfirmationResult.Errors);
        }

        var userDto = new UserDto {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email

        };
        return Result<UserDto>.Success(userDto);
    }

    public async Task<IResult> SendEmailConfirmationToken(int userId)
    {
        var user = await _userDomainService.GetUserById(userId);
        if (user == null) return Result<User>.Failed();
        return await SendEmailConfirmationToken(user);
    }
    
    public async Task<IResult> SendEmailConfirmationToken(string email)
    {
        var user = await _userDomainService.GetUserByEmail(email);
        if (user == null) return Result<User>.Failed();
        return await SendEmailConfirmationToken(user);
    }

    public async Task<IResult> SignInSpaAsync(SignInDto signInDto)
    {
        return await _userDomainService.SignInSpaAsync(signInDto.Email, signInDto.Password);
    }

    public async Task<IResult<string>> SignInApiAsync(SignInDto signInDto)
    {
        return await _userDomainService.SignInApiAsync(signInDto.Email, signInDto.Password);
    }

    public async Task<IResult> SendEmailConfirmationToken(User user)
    {
        var userConfirmationToken = await _userDomainService.GenerateEmailConfirmationTokenAsync(user.Id);

        if (!userConfirmationToken.Succeeded) return Result.Failed(userConfirmationToken.Errors);
       
        var urlParams = new Dictionary<string, string>() {
            { "userId", user.Id.ToString() },
            { "token", userConfirmationToken.Value }
        };
        var uri =_uriBuilderService.CreateConfiguredUri("/api/users/verify", urlParams);
        await _notification.Send(user.Email, user.Email, uri);
        return Result.Success();

    }
    public async Task<IResult> CreateToken(UserDto userDto) {
        var user = await _userDomainService.GetUserById(userDto.Id);
        if (user == null) return Result<string>.Failed("User not found");
        return Result<string>.Success(_tokenService.GenerateToken(user));
    }

    public async Task<IResult> ValidateUserEmailToken(int userId,  string token)
    {
        var result = await _userDomainService.ConfirmEmailAsync(userId, token);
        if (!result.Succeeded)
        {
            return Result.Failed(result.Errors);
        }
        
        return Result.Success();
    }
}