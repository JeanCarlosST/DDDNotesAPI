using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Domain.Notes;
using Notes.Infrastructure;
using Notes.Infrastructure.Repositories;

namespace Notes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

