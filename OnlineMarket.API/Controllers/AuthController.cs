using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Infrastructure.Users;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
