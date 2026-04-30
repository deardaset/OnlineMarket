using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Order
{
    public interface IDeleteOrderService
    {
        public Task RunAsync(Guid guid);
    }
}
