using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public string? PhotoUrl { get; set; }
        public List<OrderResponse> Orders { get; set; } = null!;
    }
}
