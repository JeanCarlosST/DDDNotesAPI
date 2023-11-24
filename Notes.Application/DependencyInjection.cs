using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}

