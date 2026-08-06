using System;
using Core.IGateways;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserGateway, UserGateaway>();
        services.AddTransient<IAddressRepository,AddressRepository>();
        services.AddTransient<ILocalityRepository,LocalityRepository>();
        return services;
    }
}