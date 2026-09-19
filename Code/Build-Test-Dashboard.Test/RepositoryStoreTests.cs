using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Stores;

namespace Build_Test_Dashboard.Test;

public class RepositoryStoreTests
{
    [Fact]
    public async Task RepositoryStore_StoreAsync_Throws_NotImplementedException()
    {
        // Arrange
        var repositoryStore = new RepositoryStore();
        var repository = new Repository();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => repositoryStore.StoreAsync(repository, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    [Fact]
    public async Task RepositoryStore_GetAsync_Throws_NotImplementedException()
    {
        // Arrange
        var repositoryStore = new RepositoryStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => repositoryStore.GetAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    [Fact]
    public async Task RepositoryStore_FindAsync_Throws_NotImplementedException()
    {
        // Arrange
        var repositoryStore = new RepositoryStore();
        var repository = new Repository();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => repositoryStore.FindAsync(repository, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    [Fact]
    public async Task RepositoryStore_DeleteAsync_Throws_NotImplementedException()
    {
        // Arrange
        var repositoryStore = new RepositoryStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => repositoryStore.DeleteAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }
}
