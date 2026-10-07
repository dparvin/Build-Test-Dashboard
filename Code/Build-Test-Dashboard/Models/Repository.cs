using System.ComponentModel.DataAnnotations;

namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents a repository in the build and test dashboard.
/// </summary>
public class Repository
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
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the provider.
    /// </summary>
    /// <value>
    /// The provider.
    /// </value>
    [MaxLength(50)]
    public string Provider { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the owner.
    /// </summary>
    /// <value>
    /// The owner.
    /// </value>
    [MaxLength(200)]
    public string Owner { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the project.
    /// </summary>
    /// <value>
    /// The project.
    /// </value>
    [MaxLength(200)]
    public string Project { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the repository name.
    /// </summary>
    /// <value>
    /// The repository name.
    /// </value>
    [MaxLength(200)]
    public string RepositoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the builds.
    /// </summary>
    /// <value>
    /// The builds.
    /// </value>
    public ICollection<Build> SourceBuilds { get; set; } = [];
}