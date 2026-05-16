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
            builder.ToTable("Products").HasKey(product => product.Id);

            builder.Property(product => product.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(product => product.Description)
                .HasMaxLength(1000);

            builder.Property(product => product.Price)
                .HasPrecision(18, 2);

            builder.Property(product => product.PhotoUrl)
                .HasMaxLength(2048);
        }
    }
}
