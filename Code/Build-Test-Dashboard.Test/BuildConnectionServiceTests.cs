using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.Test;

public class BuildConnectionServiceTests
{
    /// <summary>
    /// The factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildConnectionServiceTests"/> class.
    /// </summary>
    public BuildConnectionServiceTests()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// Saves the asynchronous BuildConnection.
    /// </summary>
    [Fact]
    public async Task SaveAsync_BuildConnection()
    {
        factory.RepositoryStore.FindResult = null;

        var buildConnection = new BuildConnection
        {
            Id = 0,
            Name = "PropertyGridHelpers Code Coverage",
            Provider = "GitHub"
        };

        using var scope = factory.Services.CreateScope();
        var buildConnectionService =
            scope.ServiceProvider.GetRequiredService<BuildConnectionService>();

        var savedConnection = await buildConnectionService.SaveAsync(
                buildConnection,
                TestContext.Current.CancellationToken);

        Assert.Equal(1, savedConnection.Id);
    }

    /// <summary>
    /// Gets all BuildConnections asynchronously.
    /// </summary>
    [Fact]
    public async Task GetAllAsync_BuildConnections()
    {
        factory.BuildConnectionServiceState.UseRealService = true;
        factory.BuildConnectionStore.BuildConnections =
        [
            new() {
                Id = 1,
                Name = "PropertyGridHelpers Code Coverage",
                Provider = "GitHub"
            },
            new()
            {
                Id = 2,
                Name = "PropertyGridHelpers",
                Provider = "GitHub"
            }
        ];

        using var scope = factory.Services.CreateScope();
        var buildConnectionService =
            scope.ServiceProvider.GetRequiredService<BuildConnectionService>();

        var savedConnections = await buildConnectionService.GetAllAsync(
                TestContext.Current.CancellationToken);

        Assert.Equal(2, savedConnections?.Count());
    }

    /// <summary>
    /// Gets a BuildConnection asynchronously.
    /// </summary>
    [Fact]
    public async Task GetAsync_BuildConnection()
    {
        factory.BuildConnectionServiceState.UseRealService = true;
        factory.BuildConnectionStore.BuildConnections =
        [
            new() {
                Id = 1,
                Name = "PropertyGridHelpers Code Coverage",
                Provider = "GitHub"
            },
            new()
            {
                Id = 2,
                Name = "PropertyGridHelpers",
                Provider = "GitHub"
            }
        ];

        using var scope = factory.Services.CreateScope();
        var buildConnectionService =
            scope.ServiceProvider.GetRequiredService<BuildConnectionService>();

        var savedConnection = await buildConnectionService.GetAsync(
                2,
                TestContext.Current.CancellationToken);

        Assert.Equal(2, savedConnection?.Id);
        Assert.Equal("PropertyGridHelpers", savedConnection?.Name);
        Assert.Equal("GitHub", savedConnection?.Provider);
    }

    /// <summary>
    /// Finds a BuildConnection asynchronously.
    /// </summary>
    [Fact]
    public async Task FindAsync_BuildConnection()
    {
        factory.BuildConnectionServiceState.UseRealService = true;
        factory.BuildConnectionStore.FindResult =
            new()
            {
                Id = 2,
                Name = "PropertyGridHelpers",
                Provider = "GitHub"
            };

        using var scope = factory.Services.CreateScope();
        var buildConnectionService =
            scope.ServiceProvider.GetRequiredService<BuildConnectionService>();

        var savedConnection = await buildConnectionService.FindAsync(
                "GitHub", "PropertyGridHelpers",
                TestContext.Current.CancellationToken);

        Assert.Equal(2, savedConnection?.Id);
        Assert.Equal("PropertyGridHelpers", savedConnection?.Name);
        Assert.Equal("GitHub", savedConnection?.Provider);
    }

    /// <summary>
    /// Deletes the existing build connection asynchronously.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ExistingBuildConnection()
    {
        // Arrange
        var buildConnection = new BuildConnection
        {
            Id = 42,
            Name = "BuildConnection",
            Provider = "GitHub"
        };

        using var scope = factory.Services.CreateScope();
        var buildConnectionService =
            scope.ServiceProvider.GetRequiredService<BuildConnectionService>();

        await buildConnectionService.SaveAsync(
            buildConnection,
            TestContext.Current.CancellationToken);

        // Act
        var result = await buildConnectionService.DeleteAsync(
            42,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
        Assert.Null(factory.BuildConnectionStore.StoredBuildConnection);
        Assert.Equal(1, factory.BuildConnectionStore.DeleteCallCount);
    }
}