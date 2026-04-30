using AutoMapper;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Services.Product
{
    public class DeleteProductService(IProductRepository repository, IStorageService storage) : IDeleteProductService
    {
        public async Task RunAsync(Guid guid)
        {
            var product = await repository.GetProductByIdAsync(guid);

            if (product == null)
                throw new OnlineMarketNotFoundException("Product not found");

            if (!string.IsNullOrEmpty(product.PhotoUrl))
                await storage.DeleteAsync(product.PhotoUrl);

            await repository.DeleteProductAsync(product);
        }
    }
}
