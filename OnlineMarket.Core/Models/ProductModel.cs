using OnlineMarket.SharedKernel.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Entities
{
    public class ProductModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public ProductCategory Category {  get; private set; }
        public decimal Price { get; private set; }
        public string? PhotoUrl { get; private set; }
        public List<OrderProductModel> Orders { get; set; } = null!;
        public ProductModel(string name, 
                       ProductCategory category,
                       decimal price,
                       string? description = null,
                       string photoUrl = null,
                       List<OrderProductModel>? orders = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required");
            Name = name;
            Description = description;
            Category = category;
            Price = price;
            PhotoUrl = photoUrl;
            Orders = orders!;
        }
        public void Update(string? name = null,
                           string? description = null,
                           ProductCategory? category = null,
                           decimal? price = null,
                           string? photoUrl = null)
        {
            if (name is not null) Name = name;
            if (description is not null) Description = description;
            if (category is not null) Category = category.Value;
            if (price is not null) Price = (decimal)price;
            if (photoUrl is not null) PhotoUrl = photoUrl;
        }
    }
}
