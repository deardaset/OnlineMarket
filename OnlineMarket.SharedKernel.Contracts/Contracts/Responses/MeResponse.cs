using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class MeResponse
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
