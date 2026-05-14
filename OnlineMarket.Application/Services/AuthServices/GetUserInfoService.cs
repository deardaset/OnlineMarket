using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Interfaces.Auth;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OnlineMarket.Application.Services.AuthServices
{
    public class GetUserInfoService(UserManager<AppUser> userManager) : IGetUserInfoService
    {
        public async Task<MeResponse> RunAsync(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user is null)
                throw new OnlineMarketUnauthorizedxception("Unauthorized");

            var roles = await userManager.GetRolesAsync(user);

            return new MeResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault() ?? "User"
            };
        }
    }
}
