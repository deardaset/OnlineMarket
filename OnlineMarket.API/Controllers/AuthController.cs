using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Services.Auth;
using OnlineMarket.Infrastructure.Users;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromServices] RegisterService service, [FromBody] RegisterRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] LoginService service, [FromBody] LoginRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromServices] LogoutService service)
        {
            await service.RunAsync();
            return Ok();
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> User([FromServices] GetUserInfoService service)
        {
            var result = await service.RunAsync(base.User);
            return Ok(result);
        }
    }
}
