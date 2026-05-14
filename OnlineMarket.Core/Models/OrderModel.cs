using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public List<OrderProductModel> Products { get; set; } = null!;
    }
}
