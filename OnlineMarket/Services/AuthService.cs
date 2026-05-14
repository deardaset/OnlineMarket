using Microsoft.AspNetCore.Components.Authorization;
using OnlineMarket.Client.Models;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class AuthService(HttpClient http, CookieAuthStateProvider authProvider)
{
    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var resp = await http.PostAsJsonAsync("api/auth/login", request);
        if (!resp.IsSuccessStatusCode) return false;

        authProvider.NotifyChanged();
        return true;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var resp = await http.PostAsJsonAsync("api/auth/register", request);
        if (!resp.IsSuccessStatusCode) return false;

        authProvider.NotifyChanged();
        return true;
    }

    public async Task LogoutAsync()
    {
        await http.PostAsync("api/auth/logout", null);
        authProvider.NotifyChanged();
    }

    public async Task<MeResponse?> GetMeAsync()
    {
        try { return await http.GetFromJsonAsync<MeResponse>("api/auth/user"); }
        catch { return null; }
    }
}