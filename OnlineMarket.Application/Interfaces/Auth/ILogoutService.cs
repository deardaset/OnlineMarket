using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Auth
{
    public interface ILogoutService
    {
        public Task RunAsync();
    }
}
