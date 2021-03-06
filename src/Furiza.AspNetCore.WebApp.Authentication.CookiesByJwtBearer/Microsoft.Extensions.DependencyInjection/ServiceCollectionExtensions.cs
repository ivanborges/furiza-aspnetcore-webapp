﻿using Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFurizaCookieAuthentication(this IServiceCollection services, AuthenticationConfiguration authenticationConfiguration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(authenticationConfiguration ?? throw new ArgumentNullException(nameof(authenticationConfiguration)));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    if (!string.IsNullOrWhiteSpace(authenticationConfiguration.CookieName))
                        options.Cookie.Name = authenticationConfiguration.CookieName;

                    if (!string.IsNullOrWhiteSpace(authenticationConfiguration.LoginPath))
                        options.LoginPath = authenticationConfiguration.LoginPath;

                    if (!string.IsNullOrWhiteSpace(authenticationConfiguration.AccessDeniedPath))
                        options.AccessDeniedPath = authenticationConfiguration.AccessDeniedPath;

                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = TokenMonitor.ValidateAccessTokenAsync
                    };
                });

            services.AddScoped<CookiesManager>();
            services.AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>();

            return services;
        }
    }
}