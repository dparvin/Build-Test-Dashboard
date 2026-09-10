namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents a store for managing repository credentials in the build and test dashboard.
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
    /// Gets or sets the secret token.
    /// </summary>
    /// <value>
    /// The secret token.
    /// </value>
    public string? Secret_Token { get; set; }
}