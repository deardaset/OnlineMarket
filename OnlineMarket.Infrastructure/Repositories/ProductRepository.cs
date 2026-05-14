using Amazon.S3.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Core.Models;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Entities;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Enums;
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
            await context.Products
                .Where(p => p.Id == product.Id)
                .ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }

        public async Task<(List<ProductModel>, int total)> GetAllProductsAsync(GetAllProductsParametersRequest request)
        {
            var query = context.Products.AsNoTracking();

            query = ApplyFilters(query, request);
            
            var total = await query.CountAsync();

            var products = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (mapper.Map<List<ProductModel>>(products), total);
        }

        public async Task<ProductModel?> GetProductByIdAsync(Guid guid)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == guid);
            return mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            var entity = await context.Products.FindAsync(product.Id);

            mapper.Map(product, entity);
            await context.SaveChangesAsync();
        }

        private static IQueryable<Product> ApplyFilters(IQueryable<Product> query, GetAllProductsParametersRequest request)
        {
            if (!string.IsNullOrEmpty(request.Search))
            {
                var term = $"%{request.Search.Trim()}%";

                query = query.Where(b =>
                    EF.Functions.ILike(b.Name, term)
                );
            }

            if (!string.IsNullOrEmpty(request.Category) && Enum.TryParse<ProductCategory>(request.Category, ignoreCase: true, out var parsedElement))
            {
                query = query.Where(b => b.Category == parsedElement);
            }

            query = request.Sort?.ToLower() switch
            {
                "name" => query.OrderBy(b => b.Name),
                "rarity" => query.OrderBy(b => b.Category),
                _ => query
            };

            return query;
        }
    }
}
