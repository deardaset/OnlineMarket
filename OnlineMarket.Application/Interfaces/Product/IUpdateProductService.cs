using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Product
{
    public interface IUpdateProductService
    {
        public Task<ProductResponse> RunAsync(Guid guid, UpdateProductRequest request);
    }
}
