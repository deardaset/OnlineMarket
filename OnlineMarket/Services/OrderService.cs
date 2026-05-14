using OnlineMarket.Client.Models;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class OrderService(HttpClient http)
{
    // ── User ───────────────────────────────────────────
    public async Task<List<OrderResponse>?> GetMyOrdersAsync() =>
        await http.GetFromJsonAsync<List<OrderResponse>>("api/order/my");

    public async Task<OrderResponse?> GetOrderAsync(Guid id) =>
        await http.GetFromJsonAsync<OrderResponse>($"api/order/{id}");

    public async Task<OrderResponse?> CreateOrderAsync() =>
        await http.PostAsJsonAsync("api/order", new { })
            .ContinueWith(t => t.Result.IsSuccessStatusCode
                ? t.Result.Content.ReadFromJsonAsync<OrderResponse>().Result
                : null);

    public async Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId)
    {
        var resp = await http.PostAsync($"api/order/{orderId}/product/{productId}", null);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId)
    {
        var resp = await http.DeleteAsync($"api/order/{orderId}/product/{productId}");
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> CancelOrderAsync(Guid orderId)
    {
        var resp = await http.DeleteAsync($"api/order/{orderId}");
        return resp.IsSuccessStatusCode;
    }

    // ── Admin ──────────────────────────────────────────
    public async Task<PagedResponse<OrderResponse>?> GetAllOrdersAsync(int page = 1, int pageSize = 20) =>
        await http.GetFromJsonAsync<PagedResponse<OrderResponse>>(
            $"api/order?page={page}&pageSize={pageSize}");

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
        var resp = await http.DeleteAsync($"api/order/{orderId}");
        return resp.IsSuccessStatusCode;
    }
}