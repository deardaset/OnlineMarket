using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Product
{
    public class GetAllProductsService(IProductRepository repository, IMapper mapper) : IGetAllProductsService
    {
        public async Task<PagedResponse<ProductResponse>> RunAsync()
        {
            var (products, total) = await repository.GetAllProductsAsync();

            var productsResponse = mapper.Map<List<ProductResponse>>(products);

            return new PagedResponse<ProductResponse>
            {
                Items = productsResponse,
                TotalCount = total
            };
        }
    }
}
