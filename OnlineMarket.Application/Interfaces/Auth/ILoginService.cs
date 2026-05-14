using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Auth
{
    public interface ILoginService
    {
        public Task<SignInResult> RunAsync(LoginRequest request);
    }
}
