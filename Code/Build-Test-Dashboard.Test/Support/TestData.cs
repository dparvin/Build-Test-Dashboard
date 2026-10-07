using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// Class to provide test data in the database
/// </summary>
public static class TestData
{
    /// <summary>
    /// Adds the data.
    /// </summary>
    /// <param name="repositoryStore">The repository store.</param>
    /// <param name="buildConnectionStore">The build connection store.</param>
    /// <param name="buildStore">The build store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task AddData(
        IRepositoryStore repositoryStore,
        IBuildConnectionStore buildConnectionStore,
        IBuildStore buildStore,
        CancellationToken cancellationToken = default)
    {
        await AddRepositoryData(repositoryStore, cancellationToken);
        await AddBuildConnectionData(buildConnectionStore, cancellationToken);
        await AddBuildData(buildStore, cancellationToken);
    }

    /// <summary>
    /// Adds the repository data.
    /// </summary>
    /// <param name="repositoryStore">The repository store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task AddRepositoryData(
        IRepositoryStore repositoryStore,
        CancellationToken cancellationToken = default)
    {
        var repository = new Repository
        {
            Id = 1,
            Name = "Build/Test Dashboard",
            Provider = "GitHub",
            Owner = "dparvin",
            Project = "",
            RepositoryName = "Build-Test-Dashboard"
        };
        await repositoryStore.StoreAsync(repository, cancellationToken);

        repository = new Repository
        {
            Id = 2,
            Name = "PropertyGridHelpers",
            Provider = "GitHub",
            Owner = "dparvin",
            Project = "",
            RepositoryName = "PropertyGridHelpers"
        };
        await repositoryStore.StoreAsync(repository, cancellationToken);
    }

    /// <summary>
    /// Adds the build connection data.
    /// </summary>
    /// <param name="buildConnectionStore">The build connection store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task AddBuildConnectionData(
        IBuildConnectionStore buildConnectionStore,
        CancellationToken cancellationToken = default)
    {
        var buildConnection = new BuildConnection
        {
            Id = 1,
            Name = "Build-Test-Dashboard",
            Provider = "GitHub"
        };
        await buildConnectionStore.StoreAsync(
            buildConnection,
            cancellationToken);

        buildConnection = new BuildConnection
        {
            Id = 2,
            Name = "PropertyGridHelpers Build",
            Provider = "GitHub"
        };
        await buildConnectionStore.StoreAsync(
            buildConnection,
            cancellationToken);

        buildConnection = new BuildConnection
        {
            Id = 3,
            Name = "PropertyGridHelpers Code Coverage",
            Provider = "GitHub"
        };
        await buildConnectionStore.StoreAsync(
            buildConnection,
            cancellationToken);
    }

    /// <summary>
    /// Adds the build data.
    /// </summary>
    /// <param name="buildStore">The build store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task AddBuildData(
        IBuildStore buildStore,
        CancellationToken cancellationToken)
    {
        var build = new Build
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
        };
        await buildStore.StoreAsync(build, cancellationToken);

        build = new Build
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
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Id = 2,
                    BuildId = 2,
                    Total = 150,
                    Passed = 150,
                    Failed = 0,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(55)
                },
                new TestRun
                {
                    Id = 3,
                    BuildId = 2,
                    Total = 25,
                    Passed = 24,
                    Failed = 1,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(8)
                }
            ]
        };
        await buildStore.StoreAsync(build, cancellationToken);

        build = new Build
        {
            Id = 3,
            BuildConnectionId = 2,
            SourceRepositoryId = 2,
            SourceProvider = "GitHub",
            SourceOwner = "dparvin",
            SourceRepositoryName = "PropertyGridHelpers",
            ExternalBuildId = "1",
            BuildNumber = "1000",
            Branch = "main",
            Commit = "abc123",
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Id = 4,
                    BuildId = 3,
                    Total = 100,
                    Passed = 98,
                    Failed = 1,
                    Skipped = 1,
                    Duration = TimeSpan.FromSeconds(42)
                }
            ]
        };
        await buildStore.StoreAsync(build, cancellationToken);

        build = new Build
        {
            Id = 4,
            BuildConnectionId = 2,
            SourceRepositoryId = 2,
            SourceProvider = "GitHub",
            SourceOwner = "dparvin",
            SourceRepositoryName = "PropertyGridHelpers",
            ExternalBuildId = "2",
            BuildNumber = "1001",
            Branch = "main",
            Commit = "def456",
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Id = 5,
                    BuildId = 4,
                    Total = 150,
                    Passed = 150,
                    Failed = 0,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(55)
                },
                new TestRun
                {
                    Id = 6,
                    BuildId = 4,
                    Total = 25,
                    Passed = 24,
                    Failed = 1,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(8)
                }
            ]
        };
        await buildStore.StoreAsync(build, cancellationToken);

        // Same source repository, but a different build connection.
        build = new Build
        {
            Id = 5,
            BuildConnectionId = 3,
            SourceRepositoryId = 2,
            SourceProvider = "GitHub",
            SourceOwner = "dparvin",
            SourceRepositoryName = "PropertyGridHelpers",
            ExternalBuildId = "1",
            BuildNumber = "2000",
            Branch = "main",
            Commit = "abc123",
            Status = "Succeeded"
        };
        await buildStore.StoreAsync(build, cancellationToken);
    }
}
