using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Stores;

namespace Build_Test_Dashboard.Test;

public class WindowsCredentialStoreTests
{
    [Fact]
    public async Task WindowsCredentialStore_StoreAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new WindowsCredentialStore();
        var credential = new RepositoryCredential();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.StoreAsync(credential, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    [Fact]
    public async Task WindowsCredentialStore_GetAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new WindowsCredentialStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.GetAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    [Fact]
    public async Task WindowsCredentialStore_DeleteAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new WindowsCredentialStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.DeleteAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }
}
