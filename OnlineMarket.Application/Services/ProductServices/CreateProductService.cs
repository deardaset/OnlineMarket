using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Mappings;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.ProductServices
{
    public class CreateProductService(IProductRepository repository, IStorageService storage) : ICreateProductService
    {
        public async Task<ProductResponse> RunAsync(CreateProductRequest request)
        {
            string? photoUrl = null;
            if (request.Photo is not null)
            {
                using var stream = request.Photo.OpenReadStream();
                photoUrl = await storage.UploadAsync(stream, request.Photo.FileName, request.Photo.ContentType);
            }

            var product = new ProductModel
            {
                Name = request.Name.Trim(),
                Description = NormalizeOptionalText(request.Description),
                Category = request.Category,
                Price = request.Price,
                PhotoUrl = photoUrl
            };

            await repository.CreateProductAsync(product);

            return ResponseMapper.ToProductResponse(product);
        }

        private static string? NormalizeOptionalText(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
