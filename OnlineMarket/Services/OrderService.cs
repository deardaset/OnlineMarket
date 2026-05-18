using OnlineMarket.Client.Models;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class OrderService(HttpClient http)
{
    public async Task<List<OrderResponse>?> GetMyOrdersAsync()
    {
        try
        {
            return await http.GetFromJsonAsync<List<OrderResponse>>("api/order/my");
        }
        catch
        {
            return null;
        }
    }

    public async Task<OrderResponse?> GetOrderAsync(Guid id)
    {
        try
        {
            return await http.GetFromJsonAsync<OrderResponse>($"api/order/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<OrderResponse?> CreateOrderAsync()
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/order", new { });
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<OrderResponse>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId)
    {
        try
        {
            var response = await http.PostAsync($"api/order/{orderId}/product/{productId}", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId)
    {
        try
        {
            var response = await http.DeleteAsync($"api/order/{orderId}/product/{productId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CancelOrderAsync(Guid orderId)
    {
        try
        {
            var response = await http.DeleteAsync($"api/order/{orderId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<PagedResponse<OrderResponse>?> GetAllOrdersAsync(int page = 1, int pageSize = 20)
    {
        try
        {
            return await http.GetFromJsonAsync<PagedResponse<OrderResponse>>(
                $"api/order?page={page}&pageSize={pageSize}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
        try
        {
            var response = await http.DeleteAsync($"api/order/{orderId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
