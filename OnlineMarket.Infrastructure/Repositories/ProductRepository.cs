using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Core.Models;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context, IMapper mapper) : IProductRepository
    {
        public async Task CreateProductAsync(ProductModel product)
        {
            await context.Products.AddAsync(mapper.Map<Product>(product));
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(ProductModel product)
        {
            context.Products.Remove(mapper.Map<Product>(product));
            await context.SaveChangesAsync();
        }

        public async Task<(List<ProductModel>, int total)> GetAllProductsAsync()
        {
            var query = context.Products.AsNoTracking();
            
            var total = await query.CountAsync();

            var products = await query.ToListAsync();

            return (mapper.Map<List<ProductModel>>(products), total);
            //TODO: Filters, pagination
        }

        public async Task<ProductModel?> GetProductByIdAsync(Guid guid)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == guid);
            return mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            context.Products.Update(mapper.Map<Product>(product));
        }
    }
}
