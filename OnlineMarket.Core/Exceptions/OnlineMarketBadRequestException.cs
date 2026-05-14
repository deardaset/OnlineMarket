using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketBadRequestException : OnlineMarketException
    {
        public OnlineMarketBadRequestException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
