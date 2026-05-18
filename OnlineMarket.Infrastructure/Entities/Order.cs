using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> Products { get; set; } = [];
    }
}
