using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.ProductServices
{
    public class GetAllProductsService(IProductRepository repository) : IGetAllProductsService
    {
        public async Task<PagedResponse<ProductResponse>> RunAsync(GetAllProductsParametersRequest request)
        {
            var (products, total) = await repository.GetAllProductsAsync(request);

            var productsResponse = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                PhotoUrl = p.PhotoUrl,
                Orders = p.Orders.Select(op => new OrderProductResponse 
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Price = op.Price,
                    Order = op.Order is null ? null! : new OrderResponse
                    {
                        Id = op.Order.Id,
                        UserId = op.Order.UserId
                    }
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
