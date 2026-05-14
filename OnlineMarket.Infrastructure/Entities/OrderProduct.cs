using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Entities
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
