using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OnlineMarket.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnlineMarket.Application.Services.Auth
{
    public class LoginService(SignInManager<AppUser> signInManager)
    {
        public async Task<SignInResult> RunAsync(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);
            return result;
        }
    }
}
