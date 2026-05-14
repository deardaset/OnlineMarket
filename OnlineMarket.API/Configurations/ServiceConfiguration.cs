using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Services.AuthServices;
using OnlineMarket.Application.Services.ProductServices;
using OnlineMarket.Core.Interfaces;
using OnlineMarket.Infrastructure.Repositories;

namespace OnlineMarket.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(
                    typeof(ProductRepository).Assembly,
                    typeof(CreateProductService).Assembly
                )
                .AddClasses(classes => classes.InNamespaces(
                    "OnlineMarket.Application.Services",
                    "OnlineMarket.Infrastructure.Repositories"
                ))
                .AsMatchingInterface()
                .WithScopedLifetime()
            );

            services.AddSingleton<IStorageService, StorageService>();

            return services;
        }
    }
}
