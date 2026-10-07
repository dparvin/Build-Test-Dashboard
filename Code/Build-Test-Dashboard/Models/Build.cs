using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Build_Test_Dashboard.Models;

/// <summary>
/// Represents a build in the build and test dashboard.
/// </summary>
[Index(nameof(BuildConnectionId), nameof(ExternalBuildId), IsUnique = true)]
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
    /// Gets or sets the build connection identifier.
    /// </summary>
    public int BuildConnectionId { get; set; }
    /// <summary>
    /// Gets or sets the optional source repository identifier.
    /// </summary>
    /// <remarks>
    /// This is populated when the source repository is also tracked by
    /// the dashboard. A build can still be stored when no corresponding
    /// repository record exists.
    /// </remarks>
    public int? SourceRepositoryId { get; set; }
    /// <summary>
    /// Gets or sets the source provider.
    /// </summary>
    /// <value>
    /// The source provider.
    /// </value>
    [MaxLength(50)]
    public string SourceProvider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source repository owner.
    /// </summary>
    /// <value>
    /// The source owner.
    /// </value>
    [MaxLength(200)]
    public string SourceOwner { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source repository project.
    /// </summary>
    /// <value>
    /// The source project.
    /// </value>
    [MaxLength(200)]
    public string SourceProject { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source repository name.
    /// </summary>
    /// <value>
    /// The name of the source repository.
    /// </value>
    [MaxLength(200)]
    public string SourceRepositoryName { get; set; } = string.Empty;


    /// <summary>
    /// Gets or sets the external build identifier.
    /// </summary>
    /// <value>
    /// The external build identifier.
    /// </value>
    /// <remarks>
    /// This identifies a specific build execution in the external system.
    /// If a build is run more than once, this identifier will change even
    /// if the other build data stays mostly the same.
    /// </remarks>
    [MaxLength(100)]
    public string ExternalBuildId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the build number.
    /// </summary>
    /// <value>
    /// The build number.
    /// </value>
    [MaxLength(100)]
    public string BuildNumber { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the branch.
    /// </summary>
    /// <value>
    /// The branch.
    /// </value>
    [MaxLength(500)]
    public string Branch { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the commit or changeset.
    /// </summary>
    /// <value>
    /// The commit or changeset.
    /// </value>
    [MaxLength(100)]
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
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the build connection.
    /// </summary>
    /// <value>
    /// The build connection.
    /// </value>
    public BuildConnection BuildConnection { get; set; } = null!;
    /// <summary>
    /// Gets or sets the source repository.
    /// </summary>
    /// <value>
    /// The source repository.
    /// </value>
    public Repository? SourceRepository { get; set; } = null!;
    /// <summary>
    /// Gets or sets the test runs.
    /// </summary>
    /// <value>
    /// The test runs.
    /// </value>
    public ICollection<TestRun> TestRuns { get; set; } = [];
}