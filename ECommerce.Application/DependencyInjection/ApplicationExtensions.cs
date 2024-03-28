﻿using ECommerce.Application.IServices;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IUserService, UserService>();

            // Additional application-related service registrations

            return services;
        }
    }
}
