using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Product
{
    public class UpdateProductService(IProductRepository repository, IStorageService storage, IMapper mapper) : IUpdateProductService
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

            product.Update(
                name: request.Name,
                description: request.Description,
                category: request.Category,
                price: request.Price,
                photoUrl: photoUrl
                );

            await repository.UpdateProductAsync(product);

            return mapper.Map<ProductResponse>(product);
        }
    }
}
