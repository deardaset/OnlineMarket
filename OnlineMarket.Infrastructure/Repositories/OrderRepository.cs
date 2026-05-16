using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Core.Models;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Entities;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context, IMapper mapper) : IOrderRepository
    {
        private const int MaxPageSize = 100;

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
        }

        public async Task<(List<OrderModel>, int total)> GetAllOrderAsync(GetAllOrdersParametersRequest request)
        {
            var page = Math.Max(request.Page, 1);
            var pageSize = Math.Clamp(request.PageSize, 1, MaxPageSize);

            var query = context.Orders
                .AsNoTracking()
                .Include(order => order.Products)
                    .ThenInclude(orderProduct => orderProduct.Product)
                .OrderBy(order => order.Id);

            var total = await query.CountAsync();

            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (mapper.Map<List<OrderModel>>(orders), total);
        }

        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await context.Orders
                .AsNoTracking()
                .Include(order => order.Products)
                    .ThenInclude(orderProduct => orderProduct.Product)
                .Where(order => order.UserId == userId)
                .OrderBy(order => order.Id)
                .ToListAsync();

            return mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel?> GetOrderByIdAsync(Guid guid)
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(order => order.Products)
                    .ThenInclude(orderProduct => orderProduct.Product)
                .FirstOrDefaultAsync(order => order.Id == guid);

            return order is null ? null : mapper.Map<OrderModel>(order);
        }

        public async Task UpdateOrderAsync(OrderModel order)
        {
            var entity = await context.Orders.FindAsync(order.Id);

            entity!.UserId = order.UserId;
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId)
        {
            var order = await context.Orders
                .Include(order => order.Products)
                .FirstOrDefaultAsync(order => order.Id == orderId);

            var product = await context.Products.FirstOrDefaultAsync(product => product.Id == productId);

            order!.Products.Add(new OrderProduct
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
                .Include(order => order.Products)
                .FirstOrDefaultAsync(order => order.Id == orderId);

            var item = order!.Products.FirstOrDefault(orderProduct => orderProduct.ProductId == productId);

            order.Products.Remove(item!);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
