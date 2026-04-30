using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketNotFoundException : OnlineMarketException
    {
        public OnlineMarketNotFoundException(string message) : base(message)
        {
            StatusCode = 404;
        }
    }
}
