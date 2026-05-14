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
    public class RegisterService(UserManager<AppUser> userManager, IConfiguration config) : IRegisterService   
    {
        public async Task<IdentityResult> RunAsync(OnlineMarketRegisterRequest request)
        {
            if (!Enum.TryParse<Roles>(request.Role, ignoreCase: true, out var role))
                throw new OnlineMarketBadRequestException($"Not allowed role {role}");

            if (request.Role == "Admin")
            {
                var secret = config["AdminSecret"];
                if (string.IsNullOrEmpty(secret) || request.AdminSecret != secret)
                    throw new OnlineMarketBadRequestException("Wrong Admin secret key");
            }
                        
            var user = new AppUser { UserName = request.Email, Email = request.Email };

            var result = await userManager.CreateAsync(user, request.Password);
            await userManager.AddToRoleAsync(user, request.Role);

            return result;
        }
    }
}
