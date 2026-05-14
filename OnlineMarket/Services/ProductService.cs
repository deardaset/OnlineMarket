using OnlineMarket.Client.Models;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class ProductService(HttpClient http)
{
    // ── Public ─────────────────────────────────────────
    public async Task<PagedResponse<ProductResponse>?> GetProductsAsync(
        int page = 1, int pageSize = 20, string? sort = null, string? search = null, ProductCategory? category = null)
    {
        var url = $"api/product?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrEmpty(sort)) url += $"&sort={sort}";
        if (!string.IsNullOrEmpty(search)) url += $"&search={search}";
        if (category.HasValue) url += $"&category={(int)category}";
        return await http.GetFromJsonAsync<PagedResponse<ProductResponse>>(url);
    }

    public async Task<ProductResponse?> GetProductAsync(Guid id) =>
        await http.GetFromJsonAsync<ProductResponse>($"api/product/{id}");

    // ── Admin ──────────────────────────────────────────
    public async Task<ProductResponse?> CreateProductAsync(
        CreateProductRequest request, Stream? photoStream = null, string? photoName = null)
    {
        using var form = new MultipartFormDataContent();
        form.Add(new StringContent(request.Name), "Name");
        form.Add(new StringContent(request.Description ?? ""), "Description");
        form.Add(new StringContent(((int)request.Category).ToString()), "Category");
        form.Add(new StringContent(request.Price.ToString("F2",
            System.Globalization.CultureInfo.InvariantCulture)), "Price");

        if (photoStream is not null && photoName is not null)
            form.Add(new StreamContent(photoStream), "Photo", photoName);

        var resp = await http.PostAsync("api/product", form);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<ProductResponse>();
    }

    public async Task<bool> UpdateProductAsync(Guid id,
        UpdateProductRequest request, Stream? photoStream = null, string? photoName = null)
    {
        using var form = new MultipartFormDataContent();
        form.Add(new StringContent(request.Name), "Name");
        form.Add(new StringContent(request.Description ?? ""), "Description");
        form.Add(new StringContent(((int)request.Category).ToString()), "Category");
        form.Add(new StringContent(request.Price.ToString("F2",
            System.Globalization.CultureInfo.InvariantCulture)), "Price");

        if (photoStream is not null && photoName is not null)
            form.Add(new StreamContent(photoStream), "Photo", photoName);

        var resp = await http.PutAsync($"api/product/{id}", form);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var resp = await http.DeleteAsync($"api/product/{id}");
        return resp.IsSuccessStatusCode;
    }
}