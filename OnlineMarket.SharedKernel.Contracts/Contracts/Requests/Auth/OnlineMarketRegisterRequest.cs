using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Auth
{
    public class OnlineMarketRegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? AdminSecret { get; set; }
    }
}
