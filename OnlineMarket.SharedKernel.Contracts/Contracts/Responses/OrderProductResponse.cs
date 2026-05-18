using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class OrderProductResponse
    {
        public Guid OrderId { get; set; }
        public OrderResponse? Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductResponse? Product { get; set; }
        public decimal Price { get; set; }
    }
}
