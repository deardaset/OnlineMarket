using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Interfaces.Auth;
using OnlineMarket.Application.Services.AuthServices;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Auth;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromServices] IRegisterService service, [FromBody] OnlineMarketRegisterRequest request)
        {
            var result = await service.RunAsync(request);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] ILoginService service, [FromBody] LoginRequest request)
        {
            var result = await service.RunAsync(request);
            if (!result.Succeeded)
                return Unauthorized();

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromServices] ILogoutService service)
        {
            await service.RunAsync();
            return Ok();
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo([FromServices] IGetUserInfoService service)
        {
            var result = await service.RunAsync(User);
            return Ok(result);
        }
    }
}
