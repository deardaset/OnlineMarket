using AutoMapper;
using OnlineMarket.Core.Entities;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderModel, OrderResponse>();
            CreateMap<OrderResponse, OrderModel>();
        }
    }
}
