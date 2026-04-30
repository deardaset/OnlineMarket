using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(o => o.Id);
        }
    }
}
