using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Build_Test_Dashboard.Test.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Replaces the specified implementation.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="implementation">The implementation.</param>
    /// <returns></returns>
    public static IServiceCollection Replace<TService>(
        this IServiceCollection services,
        TService implementation)
        where TService : class
    {
        services.RemoveAll<TService>();
        services.AddScoped<TService>(_ => implementation);

        return services;
    }

    /// <summary>
    /// Replaces the specified implementations.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="implementations">The implementations.</param>
    /// <returns></returns>
    public static IServiceCollection Replace<TService>(
        this IServiceCollection services,
        params TService[] implementations)
        where TService : class
    {
        services.RemoveAll<TService>();
        for (int i = 0; i < implementations.Length; i++)
            services.AddScoped<TService>(_ => implementations[i]);

        return services;
    }
}