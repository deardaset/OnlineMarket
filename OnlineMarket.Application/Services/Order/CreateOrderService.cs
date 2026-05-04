using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineMarket.Infrastructure.Entities;

namespace OnlineMarket.Application.Services.Order
{
    public class CreateOrderService(IOrderRepository repository) : ICreateOrderService
    {
        public async Task<OrderResponse> RunAsync()
        {
            Orde order = new Order();

            await repository.CreateOrderAsync(order);

            return new OrderResponse
            {
                Id = order.Id
            };
        }
    }
}
