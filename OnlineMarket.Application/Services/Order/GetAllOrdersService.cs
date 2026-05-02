using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Order
{
    public class GetAllOrdersService(IOrderRepository repository) : IGetAllOrdersService
    {
        public async Task<PagedResponse<OrderResponse>> RunAsync()
        {
            var (orders, total) = await repository.GetAllOrderAsync();

            var ordersResponse = orders.Select(o => new OrderResponse
            {
                Id = o.Id,
                Products = o.Products.Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category,
                    Price = p.Price,
                    PhotoUrl = p.PhotoUrl
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
