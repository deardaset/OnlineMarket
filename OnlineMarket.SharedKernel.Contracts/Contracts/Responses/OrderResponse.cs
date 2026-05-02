using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public List<ProductResponse> Products { get; set; } = null!;
    }
}
