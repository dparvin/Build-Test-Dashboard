using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Stores;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the test class for WindowsCredentialManager.
/// </summary>
public class WindowsCredentialManagerTests
{
    /// <summary>
    /// Windows credential manager write throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task WindowsCredentialManager_Write_Throws_NotImplementedException()
    {
        // Arrange
        var credentialManager = new WindowsCredentialManager();
        var credential = new Credential();
        // Act
        var exception = Assert.Throws<NotImplementedException>(() => credentialManager.Write("target", "Someone", credential.Secret));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    /// <summary>
    /// Windows credential manager read throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task WindowsCredentialManager_Read_Throws_NotImplementedException()
    {
        // Arrange
        var credentialManager = new WindowsCredentialManager();
        // Act
        var exception = Assert.Throws<NotImplementedException>(() => credentialManager.Read("target"));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

    /// <summary>
    /// Windows credential manager delete throws not implemented exception.
    /// </summary>
    [Fact]
    public async Task WindowsCredentialManager_Delete_Throws_NotImplementedException()
    {
        // Arrange
        var credentialManager = new WindowsCredentialManager();
        // Act
        var exception = Assert.Throws<NotImplementedException>(() => credentialManager.Delete("target"));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }
}
