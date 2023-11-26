using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(
            optionsBuilder =>
            {
                string connectionString = configuration.GetConnectionString("ConnectionString")!;
                optionsBuilder.UseSqlServer(connectionString);
            });

        return services;
    }
}
