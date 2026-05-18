using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Order;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Order
{
    public interface IGetAllOrdersService
    {
        public Task<PagedResponse<OrderResponse>> RunAsync(GetAllOrdersParametersRequest request);
    }
}
