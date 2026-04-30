using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public string? PhotoUrl { get; set; }
        public List<OrderProduct> Orders { get; set; } = [];
    }
}
