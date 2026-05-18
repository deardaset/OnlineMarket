using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Mappings;
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

            return new PagedResponse<ProductResponse>
            {
                Items = products.Select(product => ResponseMapper.ToProductResponse(product)).ToList(),
                TotalCount = total
            };
        }
    }
}
