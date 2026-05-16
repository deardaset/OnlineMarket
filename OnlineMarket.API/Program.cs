using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.API.Configurations;
using OnlineMarket.Application.Validators.Product;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Mappings;
using OnlineMarket.Infrastructure.Users;
using OnlineMarket.SharedKernel.Contracts.Enums;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var culture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<OnlineMarketExceptionHandler>();
builder.Services.AddProblemDetails();

//AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(ProductEntityProfile).Assembly);
});

//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

//Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(opts => {
    opts.Password.RequireDigit = true;
    opts.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opts => {
    opts.Cookie.Name = "auth";
    opts.Cookie.HttpOnly = true;
    opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opts.Cookie.SameSite = SameSiteMode.None;
    opts.ExpireTimeSpan = TimeSpan.FromHours(8);
    opts.SlidingExpiration = true;

    opts.Events.OnRedirectToLogin = ctx => {
        ctx.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    opts.Events.OnRedirectToAccessDenied = ctx => {
        ctx.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});


builder.Services.AddAuthorization();

builder.Services.AddCors(opts => {
    opts.AddPolicy("BlazorClient", policy => {
        policy.WithOrigins("https://localhost:7080", "http://localhost:5293")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

//Configurations
builder.Services.Configure<YandexStorageSettings>(
    builder.Configuration.GetSection("YandexStorage"));

//Services
builder.Services.AddApplicationServices();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in Enum.GetValues<Roles>())
    {
        var roleName = role.ToString();

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
;

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseCors("BlazorClient");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
