using Core.UseCases.Abstractions;
using Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IUserUseCases, UserUseCases>();
        services.AddTransient<IProfilUseCases,ProfileUseCases>();
        services.AddTransient<IProductUseCases,ProductUseCases>();
        services.AddTransient<ICartUseCases,CartUseCases>();
        return services;
    }
}