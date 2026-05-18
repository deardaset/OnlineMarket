using OnlineMarket.Client.Models;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class ProductService(HttpClient http)
{
    public async Task<PagedResponse<ProductResponse>?> GetProductsAsync(
        int page = 1,
        int pageSize = 20,
        string? sort = null,
        string? search = null,
        ProductCategory? category = null)
    {
        var query = new List<string>
        {
            $"page={page}",
            $"pageSize={pageSize}"
        };

        if (!string.IsNullOrWhiteSpace(sort))
            query.Add($"sort={Uri.EscapeDataString(sort)}");

        if (!string.IsNullOrWhiteSpace(search))
            query.Add($"search={Uri.EscapeDataString(search)}");

        if (category.HasValue)
            query.Add($"category={Uri.EscapeDataString(category.Value.ToString())}");

        try
        {
            return await http.GetFromJsonAsync<PagedResponse<ProductResponse>>($"api/product?{string.Join("&", query)}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProductResponse?> GetProductAsync(Guid id)
    {
        try
        {
            return await http.GetFromJsonAsync<ProductResponse>($"api/product/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProductResponse?> CreateProductAsync(
        CreateProductRequest request,
        Stream? photoStream = null,
        string? photoName = null,
        string? photoContentType = null)
    {
        using var form = CreateProductForm(request.Name, request.Description, request.Category, request.Price, photoStream, photoName, photoContentType);

        try
        {
            var response = await http.PostAsync("api/product", form);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductResponse>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateProductAsync(
        Guid id,
        UpdateProductRequest request,
        Stream? photoStream = null,
        string? photoName = null,
        string? photoContentType = null)
    {
        using var form = CreateProductForm(request.Name, request.Description, request.Category, request.Price, photoStream, photoName, photoContentType);

        try
        {
            var response = await http.PutAsync($"api/product/{id}", form);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        try
        {
            var response = await http.DeleteAsync($"api/product/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private static MultipartFormDataContent CreateProductForm(
        string name,
        string? description,
        ProductCategory category,
        decimal price,
        Stream? photoStream,
        string? photoName,
        string? photoContentType)
    {
        var form = new MultipartFormDataContent
        {
            { new StringContent(name), "Name" },
            { new StringContent(category.ToString()), "Category" },
            { new StringContent(price.ToString("F2", CultureInfo.InvariantCulture)), "Price" }
        };

        if (!string.IsNullOrWhiteSpace(description))
            form.Add(new StringContent(description), "Description");

        if (photoStream is not null && !string.IsNullOrWhiteSpace(photoName))
        {
            var fileContent = new StreamContent(photoStream);
            if (!string.IsNullOrWhiteSpace(photoContentType))
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(photoContentType);

            form.Add(fileContent, "Photo", photoName);
        }

        return form;
    }
}
