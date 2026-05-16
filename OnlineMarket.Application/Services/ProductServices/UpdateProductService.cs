using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Mappings;
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
            if (product is null)
                throw new OnlineMarketNotFoundException("Product not found");

            if (request.Photo is not null)
            {
                if (!string.IsNullOrEmpty(product.PhotoUrl))
                    await storage.DeleteAsync(product.PhotoUrl);

                using var stream = request.Photo.OpenReadStream();
                product.PhotoUrl = await storage.UploadAsync(stream, request.Photo.FileName, request.Photo.ContentType);
            }

            product.Name = request.Name.Trim();
            product.Description = NormalizeOptionalText(request.Description);
            product.Category = request.Category;
            product.Price = request.Price;

            await repository.UpdateProductAsync(product);

            return ResponseMapper.ToProductResponse(product, includeOrders: true);
        }

        private static string? NormalizeOptionalText(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
