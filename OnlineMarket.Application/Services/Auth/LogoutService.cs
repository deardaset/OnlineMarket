using Microsoft.AspNetCore.Identity;
using OnlineMarket.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Auth
{
    public class LogoutService(SignInManager<AppUser> signInManager)
    {
        public async Task RunAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
