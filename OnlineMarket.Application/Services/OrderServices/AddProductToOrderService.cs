using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class AddProductToOrderService(IOrderRepository orderRepository, IProductRepository productRepository) : IAddProductToOrderService
    {
        public async Task<bool> RunAsync(Guid orderId, Guid productId, Guid userId, bool isAdmin)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            if (order is null)
                throw new OnlineMarketNotFoundException("Order not found");

            EnsureCanAccess(order.UserId, userId, isAdmin);

            var product = await productRepository.GetProductByIdAsync(productId);
            if (product is null)
                throw new OnlineMarketNotFoundException("Product not found");

            if (order.Products.Any(orderProduct => orderProduct.ProductId == productId))
                return true;

            return await orderRepository.AddProductToOrderAsync(orderId, productId);
        }

        private static void EnsureCanAccess(Guid orderUserId, Guid userId, bool isAdmin)
        {
            if (!isAdmin && orderUserId != userId)
                throw new OnlineMarketForbiddenException("Access denied");
        }
    }
}
