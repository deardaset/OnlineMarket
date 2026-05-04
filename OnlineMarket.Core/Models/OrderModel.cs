using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<ProductModel> Products { get; set; } = null!;
    }
}
