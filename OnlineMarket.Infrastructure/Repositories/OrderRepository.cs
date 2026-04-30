using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineMarket.Core.Entities;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context, IMapper mapper) : IOrderRepository
    {
        public async Task CreateOrderAsync(OrderModel order)
        {
            await context.Orders.AddAsync(mapper.Map<Order>(order));
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(OrderModel order)
        {
            context.Orders.Remove(mapper.Map<Order>(order));
            await context.SaveChangesAsync();
        }

        public async Task<(List<OrderModel>, int total)> GetAllOrderAsync()
        {
            var query = context.Orders.AsNoTracking();

            var total = await query.CountAsync();

            var products = await query.ToListAsync();

            return (mapper.Map<List<OrderModel>>(products), total);
            //TODO: Pagination
        }

        public async Task<OrderModel?> GetOrderByIdAsync(Guid guid)
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == guid);
            return mapper.Map<OrderModel>(order);
        }

        public async Task UpdateOrderAsync(OrderModel order)
        {
            context.Orders.Update(mapper.Map<Order>(order));
        }
    }
}
