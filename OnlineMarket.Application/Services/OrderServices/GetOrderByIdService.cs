using OnlineMarket.Application.Interfaces.Order;
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
        public async Task<OrderResponse?> RunAsync(Guid guid)
        {
            var order = await repository.GetOrderByIdAsync(guid);
            if (order == null)
                throw new OnlineMarketNotFoundException("Order not found");

            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                Products = order.Products.Select(op => new OrderProductResponse
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
                }).ToList()
            };
        }
    }
}
