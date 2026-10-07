using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents a connection to a build provider in the build and test dashboard.
/// </summary>
[Index(nameof(Provider), nameof(Name), IsUnique = true)]
public class BuildConnection
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the provider.
    /// </summary>
    /// <value>
    /// The provider.
    /// </value>
    [MaxLength(50)]
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    // Provider-specific identifier/configuration
    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public string Configuration { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the builds.
    /// </summary>
    /// <value>
    /// The builds.
    /// </value>
    public ICollection<Build> Builds { get; set; } = [];
}