using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Auth
{
    public interface IRegisterService
    {
        public Task<IdentityResult> RunAsync(OnlineMarketRegisterRequest request);
    }
}
