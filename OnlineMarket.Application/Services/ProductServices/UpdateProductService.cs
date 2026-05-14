using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.ProductServices
{
    public class UpdateProductService(IProductRepository repository, IStorageService storage) : IUpdateProductService
    {
        public async Task<ProductResponse> RunAsync(Guid guid, UpdateProductRequest request)
        {
            var product = await repository.GetProductByIdAsync(guid);
            if (product == null)
                throw new OnlineMarketNotFoundException("Product not found");

            string? photoUrl = null;
            if (request.Photo != null)
            {
                if (!string.IsNullOrEmpty(product.PhotoUrl))
                    await storage.DeleteAsync(product.PhotoUrl);

                photoUrl = await storage.UploadAsync(request.Photo.OpenReadStream(), request.Photo.FileName, request.Photo.ContentType);
            }

            bool hasChanges = false;

            if (request.Name is { } name && name != product.Name)
                (product.Name, hasChanges) = (name, true);
            if (request.Description is { } desc && desc != product.Description)
                (product.Description, hasChanges) = (desc, true);
            if (request.Category is { } cat && cat != product.Category)
                (product.Category, hasChanges) = (cat, true);
            if (request.Price is { } price && price != product.Price)
                (product.Price, hasChanges) = (price, true);
            if (photoUrl is { } url && url != product.PhotoUrl)
                (product.PhotoUrl, hasChanges) = (url, true);

            await repository.UpdateProductAsync(product);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl,
                Orders = product.Orders.Select(op => new OrderProductResponse
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Price = op.Price,
                    Order = op.Order is null ? null! : new OrderResponse
                    {
                        Id = op.Order.Id,
                        UserId = op.Order.UserId
                    }
                }).ToList()
            };
        }
    }
}
