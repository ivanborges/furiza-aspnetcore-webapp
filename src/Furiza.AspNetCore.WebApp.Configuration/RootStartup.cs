﻿using AutoMapper;
using Furiza.AspNetCore.ScopedRoleAssignmentProvider;
using Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer;
using Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer.Services;
using Furiza.AspNetCore.WebApp.Configuration.ExceptionHandling;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.Auth;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.ReCaptcha;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.RoleAssignments;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.Roles;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.ScopedRoleAssignments;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.Users;
using Furiza.AspNetCore.WebApp.Configuration.Services;
using Furiza.Networking.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Furiza.AspNetCore.WebApp.Configuration
{
    public abstract class RootStartup
    {
        protected abstract ApplicationProfile ApplicationProfile { get; }
        protected IConfiguration Configuration { get; }

        protected RootStartup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            AddCustomServicesAtTheBeginning(services);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ApplicationProfile.DefaultCultureInfo);
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo(ApplicationProfile.DefaultCultureInfo) };
                options.RequestCultureProviders.Clear();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                //This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            var authenticationConfiguration = Configuration.TryGet<AuthenticationConfiguration>();

            services.AddFurizaNetworking();
            services.AddSingleton(ApplicationProfile);
            services.AddScoped<WebApplicationLoginManager>();
            services.AddScoped<IAccessTokenRefresher, AccessTokenRefresher>();
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create(authenticationConfiguration.SecurityProviderApiUrl);

                return RestService.For<ISecurityProviderClient>(httpClient);
            });
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create(authenticationConfiguration.SecurityProviderApiUrl);

                return RestService.For<IUsersClient>(httpClient);
            });
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create(authenticationConfiguration.SecurityProviderApiUrl);

                return RestService.For<IRolesClient>(httpClient);
            });
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create(authenticationConfiguration.SecurityProviderApiUrl);

                return RestService.For<IRoleAssignmentsClient>(httpClient);
            });
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create(authenticationConfiguration.SecurityProviderApiUrl);

                return RestService.For<IScopedRoleAssignmentsClient>(httpClient);
            });

            services.AddFurizaCookieAuthentication(authenticationConfiguration);
            services.AddFurizaUserPrincipalBuilder();
            services.AddHttpContextAccessor();
            services.AddFurizaScopedRoleAssignmentProvider(new ScopedRoleAssignmentProviderConfiguration() { SecurityProviderApiUrl = authenticationConfiguration.SecurityProviderApiUrl });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(Configuration.TryGet<ReCaptchaConfiguration>());
            services.AddTransient(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.Create("https://www.google.com/");

                return RestService.For<IReCaptchaClient>(httpClient);
            });

            AddCustomServicesAtTheEnd(services);

            if (ApplicationProfile.AutomapperAssemblies.Any())
                services.AddAutoMapper(ApplicationProfile.AutomapperAssemblies);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ApplicationProfile.ErrorPage);
                app.UseHsts();
            }

            app.UseMiddleware<AjaxExceptionMiddleware>();
            app.UseMiddleware<RefitExceptionMiddleware>();

            AddCustomMiddlewaresToTheBeginningOfThePipeline(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            AddCustomMiddlewaresToTheEndOfThePipeline(app);
        }

        #region [+] Abstract
        protected abstract void AddCustomServicesAtTheBeginning(IServiceCollection services);
        protected abstract void AddCustomServicesAtTheEnd(IServiceCollection services);
        protected abstract void AddCustomMiddlewaresToTheBeginningOfThePipeline(IApplicationBuilder app);
        protected abstract void AddCustomMiddlewaresToTheEndOfThePipeline(IApplicationBuilder app);
        #endregion
    }
}