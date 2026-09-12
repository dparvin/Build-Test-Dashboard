namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents credentials used to access a repository in the build and test dashboard.
/// </summary>
public class RepositoryCredential
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int RepositoryId { get; set; }
    /// <summary>
    /// Gets or sets the authentication type.
    /// </summary>
    /// <value>
    /// The authentication type.
    /// </value>
    public string? AuthenticationType { get; set; }
    /// <summary>
    /// Gets or sets the secret.
    /// </summary>
    /// <value>
    /// The secret.
    /// </value>
    public string? Secret { get; set; }
}