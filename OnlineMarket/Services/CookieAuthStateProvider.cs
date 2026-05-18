using Microsoft.AspNetCore.Components.Authorization;
using OnlineMarket.Client.Models;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

namespace OnlineMarket.Client.Services;

/// <summary>
/// Проверяет авторизацию, обращаясь к GET /api/auth/user.
/// Сервер использует cookie-based Identity — браузер сам шлёт куки.
/// </summary>
public class CookieAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;

    public CookieAuthStateProvider(HttpClient http) => _http = http;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var me = await _http.GetFromJsonAsync<MeResponse>("api/auth/user");
            if (me is null) return Anonymous();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, me.Id),
                new Claim(ClaimTypes.Email,          me.Email    ?? ""),
                new Claim(ClaimTypes.Name,           me.UserName ?? ""),
                new Claim(ClaimTypes.Role,           me.Role ?? "")
            };

            var identity = new ClaimsIdentity(claims, authenticationType: "cookie");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return Anonymous();
        }
    }

    public void NotifyChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private static AuthenticationState Anonymous() =>
        new(new ClaimsPrincipal(new ClaimsIdentity()));
}