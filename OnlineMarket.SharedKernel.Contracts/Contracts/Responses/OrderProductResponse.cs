using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class OrderProductResponse
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
