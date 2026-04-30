using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Product
{
    public interface IGetAllProductsService
    {
        public Task<PagedResponse<ProductResponse>> RunAsync();
    }
}
