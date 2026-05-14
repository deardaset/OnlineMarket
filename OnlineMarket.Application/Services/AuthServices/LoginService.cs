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
            var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);
            return result;
        }
    }
}
