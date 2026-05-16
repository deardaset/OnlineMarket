using OnlineMarket.Client.Models;
using System.Net.Http.Json;

namespace OnlineMarket.Client.Services;

public class AuthService(HttpClient http, CookieAuthStateProvider authProvider)
{
    public async Task<bool> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/auth/login", request);
            if (!response.IsSuccessStatusCode)
                return false;

            authProvider.NotifyChanged();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/auth/register", request);
            if (!response.IsSuccessStatusCode)
                return false;

            authProvider.NotifyChanged();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await http.PostAsync("api/auth/logout", null);
        }
        catch
        {
        }

        authProvider.NotifyChanged();
    }

    public async Task<MeResponse?> GetMeAsync()
    {
        try
        {
            return await http.GetFromJsonAsync<MeResponse>("api/auth/user");
        }
        catch
        {
            return null;
        }
    }
}
