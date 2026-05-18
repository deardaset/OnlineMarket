using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketUnauthorizedException : OnlineMarketException
    {
        public OnlineMarketUnauthorizedException() : this("Unauthorized")
        {
            StatusCode = 401;
        }
        public OnlineMarketUnauthorizedException(string message) : base(message)
        {
            StatusCode = 401;
        }
    }
}
