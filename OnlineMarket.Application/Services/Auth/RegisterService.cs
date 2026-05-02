using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OnlineMarket.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Auth
{
    public class RegisterService(UserManager<AppUser> userManager)
    {
        public async Task<IdentityResult> RunAsync(RegisterRequest request)
        {
            var user = new AppUser { UserName = request.Email };
            return await userManager.CreateAsync(user, request.Password);
        }
    }
}
