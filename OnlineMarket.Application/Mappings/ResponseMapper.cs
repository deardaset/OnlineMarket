using OnlineMarket.Core.Models;
using OnlineMarket.SharedKernel.Contracts.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Mappings
{
    public static class ResponseMapper
    {
        public static ProductResponse ToProductResponse(ProductModel product, bool includeOrders = false)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl,
                Orders = includeOrders
                    ? product.Orders.Select(ToOrderProductResponseWithOrder).ToList()
                    : []
            };
        }

        public static OrderResponse ToOrderResponse(OrderModel order, bool includeProducts = false)
        {
            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                Products = includeProducts
                    ? order.Products.Select(ToOrderProductResponseWithProduct).ToList()
                    : []
            };
        }

        private static OrderProductResponse ToOrderProductResponseWithOrder(OrderProductModel orderProduct)
        {
            return new OrderProductResponse
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Price = orderProduct.Price,
                Order = orderProduct.Order is null ? null : ToOrderResponse(orderProduct.Order)
            };
        }

        private static OrderProductResponse ToOrderProductResponseWithProduct(OrderProductModel orderProduct)
        {
            return new OrderProductResponse
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Price = orderProduct.Price,
                Product = orderProduct.Product is null ? null : ToProductResponse(orderProduct.Product)
            };
        }
    }
}
