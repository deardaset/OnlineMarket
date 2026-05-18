using AutoMapper;
using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductResponse>();
            CreateMap<ProductResponse, ProductModel>();
        }
    }
}
