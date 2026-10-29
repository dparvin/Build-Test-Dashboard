using Build_Test_Dashboard.API.Data;
using Build_Test_Dashboard.API.Stores;
using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Stores;
using Build_Test_Dashboard.Test.Support;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="BuildConnectionStore"/> class.
/// </summary>
public class BuildConnectionStoreTests
{
    /// <summary>
    /// Builds the connection store get all asynchronous returns the records.
    /// </summary>
    [Fact]
    public async Task BuildConnectionStore_GetAllAsync_Returns_The_Records()
    {
        // Arrange
        await using var context = CreateContext();
        var buildConnectionStore = new BuildConnectionStore(context);
        var buildConnection = new BuildConnection
        {
            Name = "Test Build Connection",
            Provider = "GitHub",
        };

        await buildConnectionStore.StoreAsync(
            buildConnection,
            TestContext.Current.CancellationToken);

        buildConnection = new BuildConnection
        {
            Name = "Test Build Connection 2",
            Provider = "AzureDevOps",
        };

        await buildConnectionStore.StoreAsync(
            buildConnection,
            TestContext.Current.CancellationToken);

        // Act
        var result = (await buildConnectionStore.GetAllAsync(TestContext.Current.CancellationToken)).ToArray();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("Test Build Connection", result[0].Name);
        Assert.Equal("GitHub", result[0].Provider);
        Assert.Equal(buildConnection.Id, result[1].Id);
        Assert.Equal("Test Build Connection 2", result[1].Name);
        Assert.Equal("AzureDevOps", result[1].Provider);
    }

    /// <summary>
    /// Finds the asynchronous returns repository build.
    /// </summary>
    [Theory]
    [InlineData("github", "propertyGridHelpers build")]
    [InlineData("GitHub", "PropertyGridHelpers Build")]
    [InlineData("GITHUB", "PROPERTYGRIDHELPERS BUILD")]
    public async Task BuildConnectionStore_FindAsync_ReturnsBuildConnection(
        string provider,
        string name)
    {
        // Arrange
        // Create test context and add builds for repositories 1 and 2.
        await using var context = CreateContext();

        var repositoryStore = new RepositoryStore(context);

        var buildConnectionStore = new BuildConnectionStore(context);

        var buildStore = new BuildStore(context);
        await TestData.AddData(
            repositoryStore,
            buildConnectionStore,
            buildStore,
            TestContext.Current.CancellationToken);

        // Act
        var buildConnection = await buildConnectionStore.FindAsync(
            provider,
            name,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(buildConnection);
        Assert.Equal(2, buildConnection.Id);
    }

    /// <summary>
    /// Deletes a <see cref="BuildConnection"/> asynchronously.
    /// </summary>
    [Fact]
    public async Task BuildConnectionStore_DeleteAsync()
    {
        // Arrange
        // Create test context and add builds for repositories 1 and 2.
        await using var context = CreateContext();

        var repositoryStore = new RepositoryStore(context);

        var buildConnectionStore = new BuildConnectionStore(context);

        var buildStore = new BuildStore(context);
        await TestData.AddData(
            repositoryStore,
            buildConnectionStore,
            buildStore,
            TestContext.Current.CancellationToken);

        // Act
        var result = await buildConnectionStore.DeleteAsync(
            3,
            TestContext.Current.CancellationToken);

        var build = await buildStore.GetAsync(
            5,
            TestContext.Current.CancellationToken);

        var result2 = await buildConnectionStore.DeleteAsync(
            4,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Null(build);
        Assert.True(result);
        Assert.False(result2);
    }

    /// <summary>
    /// Creates the context.
    /// </summary>
    /// <returns></returns>
    private static SqliteDashboardDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SqliteDashboardDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new SqliteDashboardDbContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }
}