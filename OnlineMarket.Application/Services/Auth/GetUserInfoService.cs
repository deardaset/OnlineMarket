using Microsoft.AspNetCore.Identity;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OnlineMarket.Application.Services.Auth
{
    public class GetUserInfoService(UserManager<AppUser> userManager)
    {
        public async Task<MeResponse?> RunAsync(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user is null)
                return null;

            return new MeResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };
        }
    }
}
