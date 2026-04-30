using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.API.Configurations;
using OnlineMarket.Application.Validators.Product;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Infrastructure.Mappings;
using OnlineMarket.Infrastructure.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts => {
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
        policy.WithOrigins("https://localhost:7080")
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

app.UseHttpsRedirection();

app.UseCors("BlazorClient");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
