﻿using System.Threading.Tasks;
using Application.Users;
using Application.Users.Dtos;
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
        return Ok();
    }

    [HttpGet, Route("sendVerificationEmail/{email?}")]
    public async Task<IActionResult> SendVerificationEmail([FromQuery] string? email)
    {
        var result = await _userAppService.SendEmailConfirmationToken(email);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }
}