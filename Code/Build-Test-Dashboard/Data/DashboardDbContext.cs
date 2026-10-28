using Build_Test_Dashboard.Contracts.Models;
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
    /// Gets the build connections.
    /// </summary>
    /// <value>
    /// The build connections.
    /// </value>
    public DbSet<BuildConnection> BuildConnections => Set<BuildConnection>();
    /// <summary>
    /// Gets the test runs.
    /// </summary>
    /// <value>
    /// The test runs.
    /// </value>
    public DbSet<TestRun> TestRuns => Set<TestRun>();

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks>
    /// <para>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run. However, it will still run when creating a compiled model.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    /// examples.
    /// </para>
    /// </remarks>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Build>(entity =>
        {
            entity.HasIndex(build => new
            {
                build.BuildConnectionId,
                build.ExternalBuildId
            })
                .IsUnique();
        });

        modelBuilder.Entity<BuildConnection>(entity =>
        {
            entity.HasIndex(connection => new
            {
                connection.Provider,
                connection.Name
            })
            .IsUnique();
        });
    }
}