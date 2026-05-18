using Amazon.S3.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Core.Exceptions;
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
        private const int MaxPageSize = 100;

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
        }

        public async Task<(List<ProductModel>, int total)> GetAllProductsAsync(GetAllProductsParametersRequest request)
        {
            var page = Math.Max(request.Page, 1);
            var pageSize = Math.Clamp(request.PageSize, 1, MaxPageSize);

            var query = ApplyFilters(context.Products.AsNoTracking(), request);

            var total = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (mapper.Map<List<ProductModel>>(products), total);
        }

        public async Task<ProductModel?> GetProductByIdAsync(Guid guid)
        {
            var product = await context.Products
                .AsNoTracking()
                .Include(product => product.Orders)
                    .ThenInclude(orderProduct => orderProduct.Order)
                .FirstOrDefaultAsync(product => product.Id == guid);

            return product is null ? null : mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            var entity = await context.Products.FindAsync(product.Id);

            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.Category = product.Category;
            entity.Price = product.Price;
            entity.PhotoUrl = product.PhotoUrl;

            await context.SaveChangesAsync();
        }

        private static IQueryable<Product> ApplyFilters(IQueryable<Product> query, GetAllProductsParametersRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var term = $"%{request.Search.Trim()}%";
                query = query.Where(product =>
                    EF.Functions.ILike(product.Name, term)
                    || (product.Description != null && EF.Functions.ILike(product.Description, term)));
            }

            if (!string.IsNullOrWhiteSpace(request.Category)
                && Enum.TryParse<ProductCategory>(request.Category, ignoreCase: true, out var category))
            {
                query = query.Where(product => product.Category == category);
            }

            return request.Sort?.Trim().ToLowerInvariant() switch
            {
                "name" => query.OrderBy(product => product.Name),
                "price" => query.OrderBy(product => product.Price),
                "-price" => query.OrderByDescending(product => product.Price),
                "category" => query.OrderBy(product => product.Category).ThenBy(product => product.Name),
                _ => query.OrderBy(product => product.Name)
            };
        }
    }
}
