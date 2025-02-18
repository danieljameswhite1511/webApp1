using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp1.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserAppService _userAppService;

    public UsersController(ILogger<UsersController> logger
        , IUserAppService userAppService
        )
    {
        _logger = logger;
        _userAppService = userAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok( await _userAppService.GetUser(1));
    }
}