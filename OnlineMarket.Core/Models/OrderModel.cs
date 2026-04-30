using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Entities
{
    public class OrderModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public List<OrderProductModel> Products { get; private set; } = null!;
    }
}
