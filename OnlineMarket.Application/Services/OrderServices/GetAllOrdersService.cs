using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Application.Mappings;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Order;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class GetAllOrdersService(IOrderRepository repository) : IGetAllOrdersService
    {
        public async Task<PagedResponse<OrderResponse>> RunAsync(GetAllOrdersParametersRequest request)
        {
            var (orders, total) = await repository.GetAllOrderAsync(request);

            return new PagedResponse<OrderResponse>
            {
                Items = orders.Select(order => ResponseMapper.ToOrderResponse(order, includeProducts: true)).ToList(),
                TotalCount = total
            };
        }
    }
}
