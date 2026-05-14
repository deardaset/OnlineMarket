using Microsoft.AspNetCore.Identity;
using OnlineMarket.Application.Interfaces.Auth;
using OnlineMarket.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.AuthServices
{
    public class LogoutService(SignInManager<AppUser> signInManager) : ILogoutService
    {
        public async Task RunAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
