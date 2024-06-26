﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Email;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;
using Movieasy.Infrastructure.Authentication;
using Movieasy.Infrastructure.Authorization;
using Movieasy.Infrastructure.Clock;
using Movieasy.Infrastructure.Email;
using Movieasy.Infrastructure.Repositories;
using AuthenticationOptions = Movieasy.Infrastructure.Authentication.AuthenticationOptions;
using IAuthenticationService = Movieasy.Application.Abstractions.Authentication.IAuthenticationService;
using AuthenticationService = Movieasy.Infrastructure.Authentication.AuthenticationService;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Infrastructure.Photos;
using Movieasy.Domain.Photos;
using Movieasy.Domain.Genres;
using Movieasy.Application.Abstractions.SignalR;
using Movieasy.Infrastructure.SignalR;
using Movieasy.Domain.Actors;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Infrastructure.Caching;

namespace Movieasy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IEmailService, EmailService>();

            AddPersistence(services, configuration);

            AddAuthentication(services, configuration);

            AddAuthorization(services);

            AddSignalR(services);

            AddCaching(services, configuration);

            return services;
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents() // A bit ugly, but removing it out of here would be even uglier
                {                                         // consider moving this outside if more than 1 event
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });

            services.AddHttpContextAccessor();

            // Uses httpcontextaccessor
            services.AddScoped<IUserContext, UserContext>();
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IActorRepository, ActorRepository>();

            // Photo (cloudinary) related 
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
            // end

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();

            services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
        }

        private static void AddSignalR(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddSingleton<INotificationService, NotificationService>();
        }

        private static void AddCaching(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Cache") ?? 
                                   throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
