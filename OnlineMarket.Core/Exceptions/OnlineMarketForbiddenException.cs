using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketForbiddenException : OnlineMarketException
    {
        public OnlineMarketForbiddenException() : this("Access denied")
        {
            StatusCode = 403;
        }
        public OnlineMarketForbiddenException(string message) : base(message)
        {
            StatusCode = 403;
        }
    }
}
