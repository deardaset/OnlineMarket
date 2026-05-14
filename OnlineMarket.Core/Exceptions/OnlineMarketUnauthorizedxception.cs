using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketUnauthorizedxception : OnlineMarketException
    {
        public OnlineMarketUnauthorizedxception(string message) : base(message)
        {
            StatusCode = 401;
        }
    }
}
