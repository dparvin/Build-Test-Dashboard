using Build_Test_Dashboard.API.Stores;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="KeyVaultCredentialStore"/> class.
/// </summary>
public class KeyVaultCredentialStoreTests
{
    /// <summary>
    /// Keys the vault credential store asynchronous throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task KeyVaultCredentialStore_StoreAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new KeyVaultCredentialStore();
        var credential = new Credential();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.StoreAsync(credential, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    /// <summary>
    /// Keys the vault credential store get asynchronous throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task KeyVaultCredentialStore_GetAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new KeyVaultCredentialStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.GetAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    /// <summary>
    /// Keys the vault credential store delete asynchronous throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task KeyVaultCredentialStore_DeleteAsync_Throws_NotImplementedException()
    {
        // Arrange
        var credentialStore = new KeyVaultCredentialStore();
        // Act
        var exception = await Assert.ThrowsAsync<NotImplementedException>(() => credentialStore.DeleteAsync(1, TestContext.Current.CancellationToken));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }
}