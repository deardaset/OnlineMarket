using AutoMapper;
using OnlineMarket.Core.Models;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Mappings
{
    public class ProductEntityProfile : Profile
    {
        public ProductEntityProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
