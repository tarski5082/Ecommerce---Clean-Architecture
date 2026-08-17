namespace Infrastructure;

using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Core.IGateways;
using Infrastructure.Gateways;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ILocalityRepository,LocalityRepository>();
        services.AddTransient<ILocalityGateaway,LocalityGateaway>();
        services.AddTransient<IAddressRepository,AddressRepository>();
        services.AddTransient<IAddressGateaway,AddressGateaway>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserGateway, UserGateaway>();
        services.AddTransient<IProductRepository,ProductRepository>();
        services.AddTransient<IProductGateway,ProductGateway>();
        services.AddTransient<ICartRepository,CartRepository>();
        services.AddTransient<ICartGateway,CartGateway>();
        services.AddTransient<ICategoryRepository,CategoryRepository>();
        services.AddTransient<ICategoryGateway,CategoryGateway>();
        
        return services;
    }
}