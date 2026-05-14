using OnlineMarket.Application.Interfaces.Order;
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
            return orders.Select(o => new OrderResponse
            {
                Id = o.Id,
                UserId = o.UserId,
                Products = o.Products.Select(op => new OrderProductResponse
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Price = op.Price,
                    Product = op.Product is null ? null! : new ProductResponse
                    {
                        Id = op.Product.Id,
                        Name = op.Product.Name,
                        Description = op.Product.Description,
                        Category = op.Product.Category,
                        Price = op.Product.Price,
                        PhotoUrl = op.Product.PhotoUrl
                    }
                }).ToList(),
            }).ToList();
        }
    }
}
