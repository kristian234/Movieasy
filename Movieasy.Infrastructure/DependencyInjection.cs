using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Email;
using Movieasy.Infrastructure.Clock;
using Movieasy.Infrastructure.Email;

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

            string connectionString = configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            return services;
        }
    }
}
