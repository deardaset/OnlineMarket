using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Interfaces
{
    public interface IProductRepository
    {
        public Task CreateProductAsync(ProductModel product);
        public Task<ProductModel?> GetProductByIdAsync(Guid guid);
        public Task<(List<ProductModel>, int total)> GetAllProductsAsync(GetAllProductsParametersRequest request);
        public Task UpdateProductAsync(ProductModel product);
        public Task DeleteProductAsync(ProductModel product);
    }
}
