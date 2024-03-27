using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandRepository, BrandRepository>()
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>()
                    .AddScoped<IOrderItemRepository, OrderItemRepository>()
                    .AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
                    .AddScoped<IProductBrandRepository, ProductBrandRepository>()
                    .AddScoped<IProductCategoryRepository, ProductCategoryRepository>()
                    .AddScoped<IRoleRepository, RoleRepository>()
                    .AddScoped<IPermissionRepository, PermissionRepository>()
                    .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
                    .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                    .AddScoped<IShoppingCartRepository, ShoppingCartRepository>()
                    .AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>()
                    .AddScoped<IUserRepository, UserRepository>();

            // Additional infrastructure-related service registrations

            return services;
        }
    }
}
