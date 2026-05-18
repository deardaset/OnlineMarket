using AutoMapper;
using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Mappings
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProductModel, OrderProductResponse>();
            CreateMap<OrderProductResponse, OrderProductModel>();
        }
    }
}
