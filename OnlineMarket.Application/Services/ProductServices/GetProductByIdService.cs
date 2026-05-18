using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Mappings;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.ProductServices
{
    public class GetProductByIdService(IProductRepository repository) : IGetProductByIdService
    {
        public async Task<ProductResponse> RunAsync(Guid guid)
        {
            var product = await repository.GetProductByIdAsync(guid);
            if (product is null)
                throw new OnlineMarketNotFoundException("Product not found");

            return ResponseMapper.ToProductResponse(product);
        }
    }
}
