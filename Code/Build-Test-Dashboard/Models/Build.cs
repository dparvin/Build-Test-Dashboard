namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents a build in the build and test dashboard.
/// </summary>
public class Build
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the repository identifier.
    /// </summary>
    /// <value>
    /// The repository identifier.
    /// </value>
    public int RepositoryId { get; set; }

    /// <summary>
    /// Gets or sets the build number.
    /// </summary>
    /// <value>
    /// The build number.
    /// </value>
    public string BuildNumber { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the branch.
    /// </summary>
    /// <value>
    /// The branch.
    /// </value>
    public string Branch { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the commit.
    /// </summary>
    /// <value>
    /// The commit.
    /// </value>
    public string Commit { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the started date and time.
    /// </summary>
    /// <value>
    /// The started date and time.
    /// </value>
    public DateTime Started { get; set; }
    /// <summary>
    /// Gets or sets the completed date and time.
    /// </summary>
    /// <value>
    /// The completed date and time.
    /// </value>
    public DateTime? Completed { get; set; }
    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string Status { get; set; } = string.Empty;
}