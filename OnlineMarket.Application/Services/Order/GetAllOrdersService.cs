using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Order
{
    public class GetAllOrdersService(IOrderRepository repository, IMapper mapper) : IGetAllOrdersService
    {
        public async Task<PagedResponse<OrderResponse>> RunAsync()
        {
            var (orders, total) = await repository.GetAllOrderAsync();

            var ordersResponse = mapper.Map<List<OrderResponse>>(orders);

            return new PagedResponse<OrderResponse>
            {
                Items = ordersResponse,
                TotalCount = total
            };
        }
    }
}
