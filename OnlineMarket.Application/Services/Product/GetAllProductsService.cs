using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Product
{
    public class GetAllProductsService(IProductRepository repository) : IGetAllProductsService
    {
        public async Task<PagedResponse<ProductResponse>> RunAsync()
        {
            var (products, total) = await repository.GetAllProductsAsync();

            var productsResponse = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                PhotoUrl = p.PhotoUrl,
                Orders = p.Orders.Select(o => new OrderResponse
                {
                    Id = o.Id
                }).ToList(),
            }).ToList();

            return new PagedResponse<ProductResponse>
            {
                Items = productsResponse,
                TotalCount = total
            };
        }
    }
}
