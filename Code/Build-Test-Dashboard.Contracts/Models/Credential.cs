namespace Build_Test_Dashboard.Contracts.Models;

/// <summary>
/// Represents credentials used to access an external service.
/// </summary>
public class Credential
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; } = string.Empty;
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