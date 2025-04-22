using System.Threading.Tasks;
using Application.Users;
using Application.Users.Dtos;
using Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp1.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase{
    
    private readonly ILogger<UsersController> _logger;
    private readonly IUserAppService _userAppService;

    public UsersController(ILogger<UsersController> logger
        , IUserAppService userAppService) {
        _logger = logger;
        _userAppService = userAppService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId) {
        return Ok( await _userAppService.GetUser(1));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() {
        return Ok(await _userAppService.GetUsers());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto) {
        var createUserResult = await _userAppService.CreateUser(createUserDto);
        if (!createUserResult.Succeeded) return BadRequest(createUserResult.Errors);
        return Ok(createUserResult.Value);
    }

    [HttpGet(), Route("verify/{userId?}/{token?}")]
    public async Task<IActionResult> VerifyUser([FromQuery] int userId, [FromQuery] string token) {
        var result = await _userAppService.ValidateUserEmailToken(userId, token);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok(true);
    }

    [HttpGet, Route("sendVerificationEmail/{email?}")]
    public async Task<IActionResult> SendVerificationEmail([FromQuery] string? email)
    {
        var result = await _userAppService.SendEmailConfirmationToken(email);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpPost, Route("sign-in")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInDto signInDto)
    {
        IResult<string> result;
        if (signInDto.SignInMethod == SignInMethod.Api) {
            result = await _userAppService.SignInApiAsync(signInDto);    
        } else {
           var spaResult =  await _userAppService.SignInSpaAsync(signInDto);
           if (spaResult.Succeeded) {
               result = Result<string>.Success("");
           }
           else {
               result = Result<string>.Failed(spaResult.Errors);
           }
        }
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok(result.Value);
    }

    [HttpGet, Route("is-authenticated")]
    public async Task<IActionResult> IsAuthenticated()
    {

        if (Request.HttpContext.User.Identity != null && Request.HttpContext.User.Identity.IsAuthenticated)
        {
            return Ok(true);
        }

        return Unauthorized();
    }
}