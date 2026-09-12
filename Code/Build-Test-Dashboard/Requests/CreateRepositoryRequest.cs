using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Requests;

/// <summary>
/// Represents a request to create a repository in the build and test dashboard, including the repository details and associated credentials.
/// </summary>
public class CreateRepositoryRequest
{
    /// <summary>
    /// Gets or sets the repository.
    /// </summary>
    /// <value>
    /// The repository.
    /// </value>
    public Repository Repository { get; set; } = new();
    /// <summary>
    /// Gets or sets the repository credential.
    /// </summary>
    /// <value>
    /// The repository credential.
    /// </value>
    public RepositoryCredential Credential { get; set; } = new();
}