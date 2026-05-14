using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Models;
using OnlineMarket.Core.Interfaces;
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
            if (request.Photo != null)
                photoUrl = await storage.UploadAsync(request.Photo.OpenReadStream(), request.Photo.FileName, request.Photo.ContentType);
            
            var product = new ProductModel
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Price = request.Price,
                PhotoUrl = photoUrl
            };

            await repository.CreateProductAsync(product);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description= product.Description,
                Category= product.Category,
                Price= product.Price,
                PhotoUrl= product.PhotoUrl
            };
        }
    }
}
