using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineMarket.Core.Models;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineMarket.Core.Exceptions;

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
            await context.Orders
                .Where(o => o.Id == order.Id)
                .ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }

        public async Task<(List<OrderModel>, int total)> GetAllOrderAsync()
        {
            var query = context.Orders
                .AsNoTracking()
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product);

            var total = await query.CountAsync();

            var products = await query.ToListAsync();

            return (mapper.Map<List<OrderModel>>(products), total);
            //TODO: Pagination
        }

        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await context.Orders
                .AsNoTracking()
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel?> GetOrderByIdAsync(Guid guid)
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(x => x.Id == guid);
            return mapper.Map<OrderModel>(order);
        }

        public async Task UpdateOrderAsync(OrderModel order)
        {
            var entity = await context.Orders.FindAsync(order.Id);

            mapper.Map(order, entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId)
        {
            var order = await context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            order.Products.Add(new OrderProduct
            {
                OrderId = orderId,
                ProductId = productId,
                Price = product.Price
            });

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId)
        {
            var order = await context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            var item = order?.Products.FirstOrDefault(op => op.ProductId == productId);
            if (item is null) return false;

            order!.Products.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
