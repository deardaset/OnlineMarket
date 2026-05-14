using AutoMapper;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineMarket.Core.Models;

namespace OnlineMarket.Application.Services.OrderServices
{
    public class CreateOrderService(IOrderRepository repository) : ICreateOrderService
    {
        public async Task<OrderResponse> RunAsync(Guid userId)
        {
            var order = new OrderModel
            {
                UserId = userId
            };

            await repository.CreateOrderAsync(order);

            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId
            };
        }
    }
}
