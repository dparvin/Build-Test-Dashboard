using Build_Test_Dashboard.API.Configuration;
using Build_Test_Dashboard.API.Data;
using Build_Test_Dashboard.API.Interfaces;
using Build_Test_Dashboard.API.Providers;
using Build_Test_Dashboard.API.Services;
using Build_Test_Dashboard.API.Stores;
using Build_Test_Dashboard.Contracts.Enums;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Stores;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.API;

/// <summary>
/// Provides extension methods for configuring dependency injection in the Build-Test-Dashboard API.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the dashboard services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddDashboardServices(
        this IServiceCollection services)
    {
        services.AddScoped<IWindowsCredentialManager, WindowsCredentialManager>();
        services.AddScoped<IRepositoryStore, RepositoryStore>();
        services.AddScoped<ICredentialStore, WindowsCredentialStore>();

        services.AddScoped<IBuildStore, BuildStore>();
        services.AddScoped<IBuildService, BuildService>();

        services.AddScoped<IBuildConnectionStore, BuildConnectionStore>();
        services.AddScoped<IBuildConnectionService, BuildConnectionService>();

        services.AddScoped<IRepositoryService, RepositoryService>();

        return services;
    }

    /// <summary>
    /// Adds the dashboard database services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Unsupported database provider: {provider}</exception>
    public static IServiceCollection AddDashboardDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var (provider, connectionString) = DatabaseConfiguration.GetDatabaseProvider(configuration);

        switch (provider)
        {
            case DatabaseProvider.Sqlite:
                services.AddDbContext<SqliteDashboardDbContext>(options =>
                {
                    options.UseSqlite(connectionString,
                        sqliteOptions =>
                        {
                            sqliteOptions.MigrationsAssembly(
                                typeof(Migrations.Sqlite.InitialCreate).Assembly.FullName);
                        });
                    //options.UseSqlite(connectionString);
                });
                services.AddScoped<DashboardDbContext>(serviceProvider =>
                    serviceProvider.GetRequiredService<SqliteDashboardDbContext>());
                break;

            case DatabaseProvider.SqlServer:
                services.AddDbContext<SqlServerDashboardDbContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly(
                                typeof(Migrations.SqlServer.InitialCreate).Assembly.FullName);
                        });
                    //options.UseSqlServer(connectionString);
                });
                services.AddScoped<DashboardDbContext>(serviceProvider =>
                    serviceProvider.GetRequiredService<SqlServerDashboardDbContext>());
                break;

            default:
                throw new InvalidOperationException(
                $"Unsupported database provider: {provider}");
        }

        return services;
    }

    /// <summary>
    /// Adds the dashboard provider services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddDashboardProviderServices(
        this IServiceCollection services)
    {
        services.AddHttpClient<GitHubRepositoryProvider>(client =>
        {
            client.BaseAddress = new Uri("https://api.github.com/");
        });
        services.AddHttpClient<AzureDevOpsRepositoryProvider>(client =>
        {
            client.BaseAddress = new Uri("https://dev.azure.com/");
        });

        services.AddScoped<IRepositoryProvider>(
            provider => provider.GetRequiredService<GitHubRepositoryProvider>());

        services.AddScoped<IRepositoryProvider>(
            provider => provider.GetRequiredService<AzureDevOpsRepositoryProvider>());

        return services;
    }

    /// <summary>
    /// Registers services specific to the Dashboard API.
    /// </summary>
    public static IServiceCollection AddDashboardApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDashboardDatabaseServices(configuration);
        services.AddDashboardProviderServices();
        services.AddDashboardServices();
        services.AddControllers();

        return services;
    }
}
