using OnlineMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Interfaces
{
    public interface IOrderRepository
    {
        public Task CreateOrderAsync(OrderModel order);
        public Task<OrderModel?> GetOrderByIdAsync(Guid guid);
        public Task<(List<OrderModel>, int total)> GetAllOrderAsync();
        public Task UpdateOrderAsync(OrderModel order);
        public Task DeleteOrderAsync(OrderModel order);
    }
}
