using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Entities;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Order
{
    public class CreateOrderService(IOrderRepository repository) : ICreateOrderService
    {
        public async Task<OrderResponse> RunAsync()
        {
            var order = new OrderModel();

            await repository.CreateOrderAsync(order);

            return new OrderResponse
            {
                Id = order.Id
            };
        }
    }
}
