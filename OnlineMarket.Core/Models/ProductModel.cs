using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get;  set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProductCategory Category {  get; set; }
        public decimal Price { get; set; }
        public string? PhotoUrl { get; set; }
        public List<OrderProductModel> Orders { get; set; } = null!;
    }
}
