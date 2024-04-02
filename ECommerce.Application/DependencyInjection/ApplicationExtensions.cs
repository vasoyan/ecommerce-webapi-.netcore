using ECommerce.Application.IServices;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenService, RefreshTokenService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IUserService, UserService>();

            // Additional application-related service registrations

            return services;
        }
    }
}
