using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class RemoveProductFromOrderService(IOrderRepository orderRepository, IProductRepository productRepository) : IRemoveProductFromOrderService
    {
        public async Task<bool> RunAsync(Guid orderId, Guid productId)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new OnlineMarketNotFoundException("Order not found");

            var product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
                throw new OnlineMarketNotFoundException("Product not found");

            return await orderRepository.RemoveProductFromOrderAsync(orderId, productId);
        }
    }
}
