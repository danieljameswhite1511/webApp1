using Application.Users.Dtos;
using Application.Users.EmailTemplates;
using Domain.auth.Services;
using Domain.Common.Notifications;
using Domain.Common.Result;
using Domain.Users;
using Domain.Users.Entities;
using Infrastructure.Urls;

namespace Application.Auth;

public class AuthAppService : IAuthAppService
{
    private readonly IAsymmetricTokenService _tokenService;
    private readonly INotification _notification;
    private readonly IUriBuilderService _uriBuilderService;
    private readonly IAuthDomainService _authDomainService;
    private readonly IUserDomainService _userDomainService;

    public AuthAppService( IAsymmetricTokenService tokenService, 
        INotification notification, 
        IUriBuilderService uriBuilderService, 
        IUserDomainService userDomainService, 
        IAuthDomainService authDomainService)
    {
        _uriBuilderService = uriBuilderService;
        _userDomainService = userDomainService;
        _authDomainService = authDomainService;
        _tokenService = tokenService;
        _notification = notification;
    }


    public async Task<IResult<UserDto>> CreateUser(CreateUserDto createUserDto) {

        if (createUserDto.Password != createUserDto.ConfirmPassword)
            return Result<UserDto>.Failed("Passwords do not match");
       
        var user = new User {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
        };

        var userCreateResult = await _authDomainService.CreateUserAsync(user);
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

    public async Task<IResult> SendEmailConfirmationToken(Guid userId) {
        var user = await _userDomainService.GetUserById(userId);
        if (user == null) return Result<User>.Failed();
        return await SendEmailConfirmationToken(user);
    }
    
    public async Task<IResult> SendEmailConfirmationToken(string email) {
        var user = await _userDomainService.GetUserByEmail(email);
        if (user == null) return Result<User>.Failed();
        return await SendEmailConfirmationToken(user);
    }

    public async Task<IResult> SendPasswordResetToken(string email){
        var user = await _userDomainService.GetUserByEmail(email);
        if (user == null) return Result<User>.Failed();
        return await SendPasswordResetToken(user);
    }

    public async Task<IResult> SignInSpaAsync(SignInDto signInDto) {
        return await _authDomainService.SignInSpaAsync(signInDto.Email, signInDto.Password, signInDto.SystemId,  signInDto.TenantId);
    }

    public async Task<IResult<string>> SignInApiAsync(SignInDto signInDto) {
        return await _authDomainService.SignInApiAsync(signInDto.Email, signInDto.Password, signInDto.SystemId,  signInDto.TenantId);
    }

    public async Task<IResult> ValidatePasswordResetToken(string email, string token){
        var result = await _authDomainService.ValidatePasswordResetRequestAsync(email, token);
        if (!result.Succeeded) {
            return Result<string>.Failed(result.Errors);
        }
        return Result.Success();
    }

    public async Task<IResult> ResetPassword(string email, string token, string password) {
        var result = await _authDomainService.ResetPasswordAsync(email, token, password);
        if (!result.Succeeded)
            return Result.Failed(result.Errors);
        return Result.Success();
    }

    public async Task<IResult> SendEmailConfirmationToken(User user) {
        var userConfirmationToken = await _authDomainService.GenerateEmailConfirmationTokenAsync(user.Id);
        if (!userConfirmationToken.Succeeded) return Result.Failed(userConfirmationToken.Errors);
        var urlParams = new Dictionary<string, string>() {
            { "userId", user.Id.ToString() },
            { "token", userConfirmationToken.Value }
        };
        var uri =_uriBuilderService.CreateConfiguredUri("/confirm-email", urlParams);
        await _notification.Send(user.Email, user.Email, HtmlTemplates.ConfirmEmailTemplate(uri));
        return Result.Success();
    }
    
    public async Task<IResult> SendPasswordResetToken(User user) {
        var userConfirmationToken = await _authDomainService.GeneratePasswordResetTokenAsync(user.Email);
        if (!userConfirmationToken.Succeeded) return Result.Failed(userConfirmationToken.Errors);
       var urlParams = new Dictionary<string, string>() {
            { "email", user.Email },
            { "token", userConfirmationToken.Value }
        };
        var uri =_uriBuilderService.CreateConfiguredUri("/request-password-reset", urlParams);
        await _notification.Send(user.Email, user.Email, HtmlTemplates.ResetPasswordTemplate(uri));
        return Result.Success();
    }
    
    public async Task<IResult> CreateToken(UserDto userDto) {
        var user = await _userDomainService.GetUserById(userDto.Id);
        if (user == null) return Result<string>.Failed("User not found");
        return Result<string>.Success(_tokenService.GenerateToken(user, userDto.SystemId,  userDto.TenantId));
    }

    public async Task<IResult> ValidateUserEmailToken(Guid userId,  string token) {
        var result = await _authDomainService.ConfirmEmailAsync(userId, token);
        if (!result.Succeeded) {
            return Result.Failed(result.Errors);
        }
        return Result.Success();
    }
}