using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Stores;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// 
/// </summary>
public class RepositoryStoreTests
{
    /// <summary>
    /// Test the RepositoryStore's store asynchronous routine.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_StoreAsync_Saves_The_Record()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);
        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        // Act
        var result = await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);

        var stored = await context.Repositories
            .SingleAsync(
                r => r.Id == result.Id,
                TestContext.Current.CancellationToken);

        Assert.Equal("Test Repository", stored.Name);
    }

    /// <summary>
    /// Tests the RepositoryStore's get asynchronous routine.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_GetAsync_Returns_The_Record()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);
        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        // Act
        var result = await repositoryStore.GetAsync(
            repository.Id,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(repository.Id, result.Id);
        Assert.Equal("Test Repository", result.Name);
        Assert.Equal("GitHub", result.Provider);
        Assert.Equal("TestOwner", result.Owner);
        Assert.Equal("", result.Project);
        Assert.Equal("TestRepository", result.RepositoryName);
    }

    /// <summary>
    /// Tests the RepositoryStore's getAll asynchronous routine.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_GetAllAsync_Returns_The_Records()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);
        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        repository = new Repository
        {
            Name = "Test Repository 2",
            Provider = "AzureDevOps",
            Owner = "TestOwner",
            Project = "TestProject",
            RepositoryName = "TestRepository2"
        };

        await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        // Act
        var result = (await repositoryStore.GetAllAsync(TestContext.Current.CancellationToken)).ToArray();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("Test Repository", result[0].Name);
        Assert.Equal("GitHub", result[0].Provider);
        Assert.Equal("TestOwner", result[0].Owner);
        Assert.Equal("", result[0].Project);
        Assert.Equal("TestRepository", result[0].RepositoryName);
        Assert.Equal(repository.Id, result[1].Id);
        Assert.Equal("Test Repository 2", result[1].Name);
        Assert.Equal("AzureDevOps", result[1].Provider);
        Assert.Equal("TestOwner", result[1].Owner);
        Assert.Equal("TestProject", result[1].Project);
        Assert.Equal("TestRepository2", result[1].RepositoryName);
    }

    /// <summary>
    /// Tests the RepositoryStore's get asynchronous returns null when the record does not exist.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_GetAsync_Returns_Null_When_Record_Does_Not_Exist()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);

        // Act
        var result = await repositoryStore.GetAsync(
            1,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests the RepositoryStore's find asynchronous routine.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_FindAsync_Finds_The_Record()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);
        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        var searchRepository = new Repository
        {
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        // Act
        var result = await repositoryStore.FindAsync(
            searchRepository,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(repository.Id, result.Id);
        Assert.Equal("Test Repository", result.Name);
    }

    /// <summary>
    /// Tests the RepositoryStore's find asynchronous returns null when record does not exist.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_FindAsync_Returns_Null_When_Record_Does_Not_Exist()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);

        var repository = new Repository
        {
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        // Act
        var result = await repositoryStore.FindAsync(
            repository,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests the RepositoryStore's delete asynchronous routine.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_DeleteAsync_Deletes_The_Record()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);

        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "GitHub",
            Owner = "TestOwner",
            Project = "",
            RepositoryName = "TestRepository"
        };

        await repositoryStore.StoreAsync(
            repository,
            TestContext.Current.CancellationToken);

        var repositoryId = repository.Id;

        // Act
        var result = await repositoryStore.DeleteAsync(
            repositoryId,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);

        var deleted = await context.Repositories
            .FindAsync(
                [repositoryId],
                TestContext.Current.CancellationToken);

        Assert.Null(deleted);
    }

    /// <summary>
    /// Repositories the store delete asynchronous returns false when record does not exist.
    /// </summary>
    [Fact]
    public async Task RepositoryStore_DeleteAsync_Returns_False_When_Record_Does_Not_Exist()
    {
        // Arrange
        await using var context = CreateContext();
        var repositoryStore = new RepositoryStore(context);

        // Act
        var result = await repositoryStore.DeleteAsync(
            1,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Creates the context.
    /// </summary>
    /// <returns></returns>
    private static DashboardDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DashboardDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new DashboardDbContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }
}
