using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Order
{
    public interface IRemoveProductFromOrderService
    {
        public Task<bool> RunAsync(Guid orderId, Guid productId);
    }
}
