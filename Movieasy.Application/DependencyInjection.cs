﻿using Microsoft.Extensions.DependencyInjection;
using Movieasy.Application.Abstractions.Behaviors;

namespace Movieasy.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}
