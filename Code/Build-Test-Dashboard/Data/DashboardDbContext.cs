using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

/// <summary>
/// Represents the database context for the build and test dashboard.
/// </summary>
/// <seealso cref="DbContext" />
public class DashboardDbContext(
    DbContextOptions options)
    : DbContext(options)
{
    /// <summary>
    /// Gets the repositories.
    /// </summary>
    /// <value>
    /// The repositories.
    /// </value>
    public DbSet<Repository> Repositories => Set<Repository>();
    /// <summary>
    /// Gets the builds.
    /// </summary>
    /// <value>
    /// The builds.
    /// </value>
    public DbSet<Build> Builds => Set<Build>();
    /// <summary>
    /// Gets the test runs.
    /// </summary>
    /// <value>
    /// The test runs.
    /// </value>
    public DbSet<TestRun> TestRuns => Set<TestRun>();
}