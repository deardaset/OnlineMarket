using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Entities
{
    public class OrderProductModel
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = null!;
        public decimal Price { get; private set; }
        public OrderProductModel(Guid orderId, 
                            Guid productId,
                            string productName,
                            decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
        }
    }
}
