using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Entities
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<ProductModel> Products { get; set; } = null!;
    }
}
