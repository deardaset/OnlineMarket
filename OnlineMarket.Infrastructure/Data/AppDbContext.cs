using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.Infrastructure.Entities;
using OnlineMarket.Infrastructure.Data.Mappings;

namespace OnlineMarket.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new OrderMap());

            builder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId});

            builder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(op => op.Products)
                .HasForeignKey(op => op.OrderId);
            builder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(op => op.Orders)
                .HasForeignKey(op => op.ProductId);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .Properties<Enum>()
                .HaveConversion<string>();
        }
    }
}
