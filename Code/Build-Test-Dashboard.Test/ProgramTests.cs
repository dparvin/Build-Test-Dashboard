using Build_Test_Dashboard.Configuration;
using Build_Test_Dashboard.Enums;
using Microsoft.Extensions.Configuration;

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
}
