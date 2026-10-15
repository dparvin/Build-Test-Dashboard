using Build_Test_Dashboard.Configuration;
using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Providers;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Stores;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard;

public static class DependencyInjection
{
    public static IServiceCollection AddDashboardServices(
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
                                typeof(Build_Test_Dashboard.Migrations.SQLite.InitialCreate).Assembly.FullName);
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
                                typeof(Build_Test_Dashboard.Migrations.SqlServer.InitialCreate).Assembly.FullName);
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
    /// Registers services specific to the Dashboard API.
    /// </summary>
    public static IServiceCollection AddDashboardApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDashboardServices(configuration);
        services.AddControllers();

        return services;
    }
}
