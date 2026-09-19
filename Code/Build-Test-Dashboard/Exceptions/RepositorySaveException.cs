namespace Build_Test_Dashboard.Exceptions;

/// <summary>
/// Represents an exception that occurs when saving a repository fails
/// and the subsequent rollback also fails.
/// </summary>
/// <param name="message">The message.</param>
/// <param name="innerException">The inner exception.</param>
/// <param name="rollbackException">The rollback exception.</param>
/// <remarks>
/// Initializes a new instance of the <see cref="RepositorySaveException"/> class.
/// </remarks>
/// <seealso cref="System.Exception" />
public class RepositorySaveException(
    string message,
    Exception innerException,
    Exception rollbackException) : Exception(message, innerException)
{

    /// <summary>
    /// Gets the rollback exception.
    /// </summary>
    /// <value>
    /// The rollback exception.
    /// </value>
    public Exception RollbackException { get; } = rollbackException;
}