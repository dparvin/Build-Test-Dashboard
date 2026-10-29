using Build_Test_Dashboard.API.Exceptions;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the test class for RepositorySaveException.
/// </summary>
public class RepositorySaveExceptionTests
{
    /// <summary>
    /// Repositories the save exception initializes correctly.
    /// </summary>
    [Fact]
    public void RepositorySaveException_Initializes_Correctly()
    {
        // Arrange
        var message = "An error occurred while saving the repository.";
        var innerException = new Exception("Inner exception message.");
        var rollbackException = new Exception("Rollback exception message.");
        // Act
        var exception = new RepositorySaveException(message, innerException, rollbackException);
        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
        Assert.Equal(rollbackException, exception.RollbackException);
        { }
    }
}
