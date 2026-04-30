using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Product
{
    public interface ICreateProductService
    {
        public Task<ProductResponse> RunAsync(CreateProductRequest request);
    }
}
