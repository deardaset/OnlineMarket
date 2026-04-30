using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
