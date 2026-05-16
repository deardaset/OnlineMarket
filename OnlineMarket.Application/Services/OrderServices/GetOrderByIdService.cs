using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Application.Mappings;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class GetOrderByIdService(IOrderRepository repository) : IGetOrderByIdService
    {
        public async Task<OrderResponse> RunAsync(Guid guid, Guid userId, bool isAdmin)
        {
            var order = await repository.GetOrderByIdAsync(guid);
            if (order is null)
                throw new OnlineMarketNotFoundException("Order not found");

            EnsureCanAccess(order.UserId, userId, isAdmin);

            return ResponseMapper.ToOrderResponse(order, includeProducts: true);
        }

        private static void EnsureCanAccess(Guid orderUserId, Guid userId, bool isAdmin)
        {
            if (!isAdmin && orderUserId != userId)
                throw new OnlineMarketForbiddenException("Access denied");
        }
    }
}
