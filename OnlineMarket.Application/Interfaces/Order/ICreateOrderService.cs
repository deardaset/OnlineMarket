using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Order
{
    public interface ICreateOrderService
    {
        public Task<OrderResponse> RunAsync();
    }
}
