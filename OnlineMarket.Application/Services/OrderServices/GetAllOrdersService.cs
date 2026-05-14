using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class GetAllOrdersService(IOrderRepository repository) : IGetAllOrdersService
    {
        public async Task<PagedResponse<OrderResponse>> RunAsync()
        {
            var (orders, total) = await repository.GetAllOrderAsync();

            var ordersResponse = orders.Select(o => new OrderResponse
            {
                Id = o.Id,
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

            return new PagedResponse<OrderResponse>
            {
                Items = ordersResponse,
                TotalCount = total
            };
        }
    }
}
