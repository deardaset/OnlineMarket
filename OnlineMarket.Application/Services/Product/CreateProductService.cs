using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Entities;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Product
{
    public class CreateProductService(IProductRepository repository, IStorageService storage, IMapper mapper) : ICreateProductService
    {
        public async Task<ProductResponse> RunAsync(CreateProductRequest request)
        {
            string? photoUrl = null;
            if (request.Photo != null)
                photoUrl = await storage.UploadAsync(request.Photo.OpenReadStream(), request.Photo.FileName, request.Photo.ContentType);
            
            var product = new ProductModel(request.Name, request.Category, request.Price, photoUrl);

            await repository.CreateProductAsync(product);

            return mapper.Map<ProductResponse>(product);
        }
    }
}
