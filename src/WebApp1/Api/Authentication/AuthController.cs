using System.Threading.Tasks;
using Application.Auth;
using Application.Users.Dtos;
using Domain.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Api.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

    [HttpPost, Route("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto) {
        var createUserResult = await _authAppService.CreateUser(createUserDto);
        if (!createUserResult.Succeeded) return BadRequest(createUserResult.Errors);
        return Ok(createUserResult.Value);
    }

    [HttpGet, Route("verify/{userId?}/{token?}")]
    public async Task<IActionResult> VerifyUser([FromQuery] int userId, [FromQuery] string token) {
        var result = await _authAppService.ValidateUserEmailToken(userId, token);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok(true);
    }

    [HttpGet, Route("sendVerificationEmail/{email?}")]
    public async Task<IActionResult> SendVerificationEmail([FromQuery] string email) {
        var result = await _authAppService.SendEmailConfirmationToken(email);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpGet, Route("sendPasswordResetEmail/{email?}")]
    public async Task<IActionResult> SendPasswordResetEmail([FromQuery] string email) {
        var result = await _authAppService.SendPasswordResetToken(email);
        if(!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpGet, Route("request-password-reset/{email?}/{token?}")]
    public async Task<IActionResult> RequestPasswordReset([FromQuery] string email, [FromQuery] string token) {
        var result = await _authAppService.ValidatePasswordResetToken(email, token);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpPost, Route("reset-password")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetRequestDto passwordResetRequest) {
        var result = await _authAppService.ResetPassword(passwordResetRequest.Email, passwordResetRequest.Token, passwordResetRequest.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }
    
    [HttpPost, Route("sign-in")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInDto signInDto)
    {
        IResult<string> result;
        if (signInDto.SignInMethod == SignInMethod.Api) {
            result = await _authAppService.SignInApiAsync(signInDto);    
        } else {
           var spaResult =  await _authAppService.SignInSpaAsync(signInDto);
           if (spaResult.Succeeded) {
               result = Result<string>.Success("");
           } else {
               result = Result<string>.Failed(spaResult.Errors);
           }
        }
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok(result.Value);
    }

    [HttpGet, Route("is-authenticated")]
    public async Task<IActionResult> IsAuthenticated() {
        if (Request.HttpContext.User.Identity != null && Request.HttpContext.User.Identity.IsAuthenticated) { 
            return Ok(true);
        }
        return Unauthorized();
    }
}