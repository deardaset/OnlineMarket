using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Application.Interfaces.Auth;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Auth;
using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.AuthServices
{
    public class RegisterService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config) : IRegisterService
    {
        public async Task<IdentityResult> RunAsync(OnlineMarketRegisterRequest request)
        {
            if (!Enum.TryParse<Roles>(request.Role, ignoreCase: true, out var role))
                throw new OnlineMarketBadRequestException($"Not allowed role {request.Role}");

            var roleName = role.ToString();

            if (role == Roles.Admin)
            {
                var secret = config["AdminSecret"];
                if (string.IsNullOrEmpty(secret) || request.AdminSecret != secret)
                    throw new OnlineMarketBadRequestException("Wrong Admin secret key");
            }

            var email = request.Email.Trim();
            var user = new AppUser { UserName = email, Email = email };

            var createResult = await userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                return createResult;

            var roleResult = await userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                return roleResult;
            }

            await signInManager.SignInAsync(user, isPersistent: true);

            return IdentityResult.Success;
        }
    }
}
