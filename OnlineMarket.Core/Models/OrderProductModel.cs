using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Models
{
    public class OrderProductModel
    {
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
