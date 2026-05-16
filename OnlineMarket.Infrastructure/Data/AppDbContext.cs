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
            builder.ApplyConfiguration(new OrderProductMap());
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
