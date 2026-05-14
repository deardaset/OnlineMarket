using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Auth
{
    public interface IGetUserInfoService
    {
        public Task<MeResponse> RunAsync(ClaimsPrincipal principal);
    }
}
