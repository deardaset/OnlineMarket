using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Order
{
    public class DeleteOrderService(IOrderRepository repository) : IDeleteOrderService
    {
        public async Task RunAsync(Guid guid)
        {
            var order = await repository.GetOrderByIdAsync(guid);

            if (order == null)
                throw new OnlineMarketNotFoundException("Order not found");

            await repository.DeleteOrderAsync(order);
        }
    }
}
