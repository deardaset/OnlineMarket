using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Data.Mappings
{
    public class OrderProductMap : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(orderProduct => new { orderProduct.OrderId, orderProduct.ProductId });

            builder.Property(orderProduct => orderProduct.Price)
                .HasPrecision(18, 2);

            builder.HasOne(orderProduct => orderProduct.Order)
                .WithMany(order => order.Products)
                .HasForeignKey(orderProduct => orderProduct.OrderId);

            builder.HasOne(orderProduct => orderProduct.Product)
                .WithMany(product => product.Orders)
                .HasForeignKey(orderProduct => orderProduct.ProductId);
        }
    }
}
