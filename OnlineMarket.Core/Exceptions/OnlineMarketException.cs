using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketException(string message) : Exception(message)
    {
        public int StatusCode = 500;
    }
}
