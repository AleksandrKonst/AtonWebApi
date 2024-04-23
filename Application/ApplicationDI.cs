using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Конфигурация сервисов Appliction слоя
/// </summary>
public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        var assemply = typeof(ApplicationDI).Assembly;

        services.AddMediatR(x => x.RegisterServicesFromAssembly(assemply));
        services.AddValidatorsFromAssembly(assemply);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAutoMapper(typeof(ApplicationDI));
        
        return services;
    }
}