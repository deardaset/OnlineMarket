using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Interfaces
{
    public interface IOrderRepository
    {
        public Task CreateOrderAsync(OrderModel order);
        public Task<OrderModel?> GetOrderByIdAsync(Guid guid);
        public Task<(List<OrderModel>, int total)> GetAllOrderAsync(GetAllOrdersParametersRequest request);
        public Task<List<OrderModel>> GetOrdersByUserIdAsync(Guid userId);
        public Task UpdateOrderAsync(OrderModel order);
        public Task DeleteOrderAsync(OrderModel order);
        public Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId);
        public Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId);
    }
}
