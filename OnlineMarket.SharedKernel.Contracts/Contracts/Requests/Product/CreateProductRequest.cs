using Microsoft.AspNetCore.Http;
using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
