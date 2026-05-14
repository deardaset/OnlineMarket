using Microsoft.AspNetCore.Http;

namespace OnlineMarket.Client.Models;

// ── Enums ──────────────────────────────────────────────
public enum ProductCategory
{
    Food,
    Grocery,
    Drinks,
    HouseHold,
    PersonalCare,
    BabyProducts,
    Medicine
}

// ── Responses (mirror SharedKernel.Contracts) ──────────
public class MeResponse
{
    public string Id { get; set; } = "";
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Role { get; set; }
}

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
    public string? PhotoUrl { get; set; }
    public List<OrderProductResponse> Orders { get; set; } = new();
}

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<OrderProductResponse> Products { get; set; } = new();
}

public class OrderProductResponse
{
    public Guid OrderId { get; set; }
    public OrderResponse Order { get; set; } = null!;
    public Guid ProductId { get; set; }
    public ProductResponse Product { get; set; } = null!;
    public decimal Price { get; set; }
}

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

// ── Requests ───────────────────────────────────────────
public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegisterRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    /// <summary>"User" или "Admin"</summary>
    public string Role { get; set; } = "User";

    /// <summary>Требуется только при Role == "Admin". Сервер проверяет совпадение с конфигурационным ключом.</summary>
    public string? AdminSecret { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
    // Photo отправляется отдельно как multipart
}

public class UpdateProductRequest
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
}