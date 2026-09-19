using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Stores;

namespace Build_Test_Dashboard.Test;

public class WindowsCredentialManagerTests
{
    [Fact]
    public async Task WindowsCredentialManager_Write_Throws_NotImplementedException()
    {
        // Arrange
        var credentialManager = new WindowsCredentialManager();
        var credential = new RepositoryCredential();
        // Act
        var exception = Assert.Throws<NotImplementedException>(() => credentialManager.Write("target", "Someone", credential.Secret));
        // Assert
        Assert.Equal("The method or operation is not implemented.", exception.Message);
    }

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
