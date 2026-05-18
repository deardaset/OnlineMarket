using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OnlineMarket.Application.Interfaces.Auth;
using OnlineMarket.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnlineMarket.Application.Services.AuthServices
{
    public class LoginService(SignInManager<AppUser> signInManager) : ILoginService
    {
        public async Task<SignInResult> RunAsync(LoginRequest request)
        {
            return await signInManager.PasswordSignInAsync(
                request.Email.Trim(),
                request.Password,
                isPersistent: true,
                lockoutOnFailure: false);
        }
    }
}
