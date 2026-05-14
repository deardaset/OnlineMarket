using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Order
{
    public interface IAddProductToOrderService
    {
        public Task<bool> RunAsync(Guid orderId, Guid productId);
    }
}
