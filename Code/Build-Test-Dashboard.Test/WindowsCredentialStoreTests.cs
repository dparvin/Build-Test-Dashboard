using Build_Test_Dashboard.API.Stores;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="WindowsCredentialStore"/> class.
/// </summary>
public class WindowsCredentialStoreTests
{
    /// <summary>
    /// Windows credential store asynchronous throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task WindowsCredentialStore_StoreAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new WindowsCredentialStore();
        var credential = new Credential();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.StoreAsync(credential, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    /// <summary>
    /// Windows credential store get asynchronous throws not implemented exception.
    /// </summary>
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

    /// <summary>
    /// Windows credential store delete asynchronous throws not implemented exception.
    /// </summary>
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
