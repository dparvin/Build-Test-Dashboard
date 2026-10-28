using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Tests for <see cref="BuildService"/>
/// </summary>
public class BuildServiceTests
{
    /// <summary>
    /// The factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildServiceTests"/> class.
    /// </summary>
    public BuildServiceTests()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// Saves the build asynchronously.
    /// </summary>
    [Fact]
    public async Task SaveAsync()
    {
        factory.BuildStore.FindResult = null;


        var request = new Build
        {
            BuildConnectionId = 1,
            SourceRepositoryId = 1,
            SourceProvider = "GitHub",
            SourceOwner = "dparvin",
            SourceRepositoryName = "Build-Test-Dashboard",
            ExternalBuildId = "1",
            BuildNumber = "100",
            Branch = "main",
            Commit = "abc123",
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Total = 100,
                    Passed = 98,
                    Failed = 1,
                    Skipped = 1,
                    Duration = TimeSpan.FromSeconds(42)
                }
            ]
        };

        using var scope = factory.Services.CreateScope();
        var buildService =
            scope.ServiceProvider.GetRequiredService<BuildService>();

        var results = buildService.SaveAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(1, results.Id);

        Assert.Equal(1, factory.BuildStore.StoreCallCount);
    }

    /// <summary>
    /// Saves the build asynchronously.
    /// </summary>
    [Fact]
    public async Task GetAllAsync()
    {
        factory.BuildServiceState.UseRealService = true;
        factory.BuildStore.Builds =
        [
            new Build
            {
                Id = 1,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "1",
                BuildNumber = "100",
                Branch = "main",
                Commit = "abc123",
                Status = "Succeeded",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 1,
                        BuildId = 1,
                        Total = 100,
                        Passed = 98,
                        Failed = 1,
                        Skipped = 1,
                        Duration = TimeSpan.FromSeconds(42)
                    }
                ]
            },
            new Build
            {
                Id = 2,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "2",
                BuildNumber = "101",
                Branch = "main",
                Commit = "def456",
                Status = "Failed",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 2,
                        BuildId = 2,
                        Total = 100,
                        Passed = 90,
                        Failed = 5,
                        Skipped = 5,
                        Duration = TimeSpan.FromSeconds(50)
                    }
                ]
            }
        ];

        using var scope = factory.Services.CreateScope();
        var buildService =
            scope.ServiceProvider.GetRequiredService<BuildService>();

        var results = buildService.GetAllAsync(
                1,
                TestContext.Current.CancellationToken);

        Assert.Equal(2, results.Id);

        Assert.Equal(1, factory.BuildStore.GetAllCallCount);
    }

    /// <summary>
    /// Finds the build asynchronously.
    /// </summary>
    [Fact]
    public async Task FindAsync()
    {
        factory.BuildServiceState.UseRealService = true;
        factory.BuildStore.Builds =
        [
            new Build
            {
                Id = 1,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "1",
                BuildNumber = "100",
                Branch = "main",
                Commit = "abc123",
                Status = "Succeeded",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 1,
                        BuildId = 1,
                        Total = 100,
                        Passed = 98,
                        Failed = 1,
                        Skipped = 1,
                        Duration = TimeSpan.FromSeconds(42)
                    }
                ]
            },
            new Build
            {
                Id = 2,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "2",
                BuildNumber = "101",
                Branch = "main",
                Commit = "def456",
                Status = "Failed",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 2,
                        BuildId = 2,
                        Total = 100,
                        Passed = 90,
                        Failed = 5,
                        Skipped = 5,
                        Duration = TimeSpan.FromSeconds(50)
                    }
                ]
            }
        ];

        using var scope = factory.Services.CreateScope();
        var buildService =
            scope.ServiceProvider.GetRequiredService<BuildService>();

        var results = buildService.FindAsync(
                1,
                "1",
                TestContext.Current.CancellationToken);

        Assert.Equal(3, results.Id);

        Assert.Equal(1, factory.BuildStore.FindCallCount);
    }

    /// <summary>
    /// Deletes the build asynchronously.
    /// </summary>
    [Fact]
    public async Task DeleteAsync()
    {
        factory.BuildServiceState.UseRealService = true;
        factory.BuildStore.Builds =
        [
            new Build
            {
                Id = 1,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "1",
                BuildNumber = "100",
                Branch = "main",
                Commit = "abc123",
                Status = "Succeeded",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 1,
                        BuildId = 1,
                        Total = 100,
                        Passed = 98,
                        Failed = 1,
                        Skipped = 1,
                        Duration = TimeSpan.FromSeconds(42)
                    }
                ]
            },
            new Build
            {
                Id = 2,
                BuildConnectionId = 1,
                SourceRepositoryId = 1,
                SourceProvider = "GitHub",
                SourceOwner = "dparvin",
                SourceRepositoryName = "Build-Test-Dashboard",
                ExternalBuildId = "2",
                BuildNumber = "101",
                Branch = "main",
                Commit = "def456",
                Status = "Failed",
                TestRuns =
                [
                    new TestRun
                    {
                        Id = 2,
                        BuildId = 2,
                        Total = 100,
                        Passed = 90,
                        Failed = 5,
                        Skipped = 5,
                        Duration = TimeSpan.FromSeconds(50)
                    }
                ]
            }
        ];

        using var scope = factory.Services.CreateScope();
        var buildService =
            scope.ServiceProvider.GetRequiredService<BuildService>();

        var results = await buildService.DeleteAsync(
                1,
                TestContext.Current.CancellationToken);

        Assert.True(results);

        Assert.Equal(1, factory.BuildStore.DeleteCallCount);
    }
}