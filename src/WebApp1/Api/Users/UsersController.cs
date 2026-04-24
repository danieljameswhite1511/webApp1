using System.Threading.Tasks;
using Application.Users;
using Application.Users.Dtos;
using Domain.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp1.Api.Users;

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

}