using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineMarket.Client;
using OnlineMarket.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient(new CookieHandler
{
    InnerHandler = new HttpClientHandler()
})
{
    BaseAddress = new Uri("https://localhost:7071/"),
});

// регистрируем конкретный класс
builder.Services.AddScoped<CookieAuthStateProvider>();

// говорим Blazor использовать его как стандартный провайдер
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CookieAuthStateProvider>());

builder.Services.AddAuthorizationCore();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

await builder.Build().RunAsync();





