using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class DeleteOrderService(IOrderRepository repository) : IDeleteOrderService
    {
        public async Task RunAsync(Guid guid, Guid userId, bool isAdmin)
        {
            var order = await repository.GetOrderByIdAsync(guid);

            if (order is null)
                throw new OnlineMarketNotFoundException("Order not found");

            if (!isAdmin && order.UserId != userId)
                throw new OnlineMarketForbiddenException("Access denied");

            await repository.DeleteOrderAsync(order);
        }
    }
}
