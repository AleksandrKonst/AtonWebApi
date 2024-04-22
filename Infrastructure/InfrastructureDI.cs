using Domain.Interfaces;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AtonDataContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        
        return services;
    }
}