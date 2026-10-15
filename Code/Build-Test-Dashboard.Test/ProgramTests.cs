using Build_Test_Dashboard.Configuration;
using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.Test;

public class ProgramTests
{
    /// <summary>
    /// Tests the when provider is null.
    /// </summary>
    [Fact]
    public void Test_When_Provider_Is_Missing()
    {
        var configuration = new ConfigurationBuilder()
            .Build();

        var (provider, connectionString) =
            DatabaseConfiguration.GetDatabaseProvider(configuration);

        Assert.Equal(DatabaseProvider.Sqlite, provider);
        Assert.Null(connectionString);
    }

    /// <summary>
    /// Tests the when provider is SQL server.
    /// </summary>
    [Fact]
    public void Test_When_Provider_Is_SqlServer()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Provider"] = "SqlServer",
                ["Database:ConnectionString"] = "test-server"
            })
            .Build();

        var (provider, connectionString) =
            DatabaseConfiguration.GetDatabaseProvider(configuration);

        Assert.Equal(DatabaseProvider.SqlServer, provider);
        Assert.Equal("test-server", connectionString);
    }

    /// <summary>
    /// Tests the when provider is sqlite.
    /// </summary>
    [Fact]
    public void Test_When_Provider_Is_Sqlite()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Provider"] = "Sqlite",
                ["Database:ConnectionString"] = "test.sqlite"
            })
            .Build();

        var (provider, connectionString) =
            DatabaseConfiguration.GetDatabaseProvider(configuration);

        Assert.Equal(DatabaseProvider.Sqlite, provider);
        Assert.Equal("test.sqlite", connectionString);
    }

    /// <summary>
    /// Tests the dependencies when provider is SQL server.
    /// </summary>
    [Fact]
    public void Test_Dependencies_When_Provider_Is_SqlServer()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Provider"] = "SqlServer",
                ["Database:ConnectionString"] = "test-server"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();

        services.AddDashboardServices(configuration);

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<SqlServerDashboardDbContext>();

        Assert.IsType<SqlServerDashboardDbContext>(context);

        var dashboardContext = scope.ServiceProvider
            .GetRequiredService<DashboardDbContext>();

        Assert.Same(context, dashboardContext);
    }

    /// <summary>
    /// Tests the dependencies when provider is unsupported.
    /// </summary>
    [Fact]
    public void Test_Dependencies_When_Provider_Is_Unsupported()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Provider"] = "999",
                ["Database:ConnectionString"] = "test"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();

        var exception = Assert.Throws<InvalidOperationException>(
            () => services.AddDashboardServices(configuration));

        Assert.Contains("Unsupported database provider", exception.Message);
    }
}
