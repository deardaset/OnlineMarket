using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Application.Mappings;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class GetMyOrdersService(IOrderRepository repository) : IGetMyOrdersService
    {
        public async Task<List<OrderResponse>> RunAsync(Guid userId)
        {
            var orders = await repository.GetOrdersByUserIdAsync(userId);
            return orders.Select(order => ResponseMapper.ToOrderResponse(order, includeProducts: true)).ToList();
        }
    }
}
