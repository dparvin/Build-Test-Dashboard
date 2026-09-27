using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// The build Store, used to access the build information
/// </summary>
/// <seealso cref="IBuildStore" />
public class BuildStore(DashboardDbContext context) : IBuildStore
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly DashboardDbContext context = context;

    /// <summary>
    /// Stores the build asynchronously.
    /// </summary>
    /// <param name="build">The build.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The stored build.
    /// </returns>
    public async Task<Build> StoreAsync(
        Build build,
        CancellationToken cancellationToken = default)
    {
        context.Builds.Add(build);

        await context.SaveChangesAsync(cancellationToken);

        return build;
    }

    /// <summary>
    /// Gets a build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    public async Task<Build?> GetAsync(
        int buildId,
        CancellationToken cancellationToken = default) =>
        await context.Builds
            .FirstOrDefaultAsync(
                build =>
                    build.Id == buildId,
                cancellationToken);

    /// <summary>
    /// Gets all builds for a repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the builds for a repository.
    /// </returns>
    public async Task<IEnumerable<Build>> GetAllAsync(
        int repositoryId,
        CancellationToken cancellationToken = default) =>
        await context.Builds
            .Where(build => build.RepositoryId == repositoryId)
            .OrderBy(build => build.Id)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Finds the build asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="externalBuildId">The external build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Build?> FindAsync(
        int repositoryId,
        string externalBuildId,
        CancellationToken cancellationToken = default) =>
        await context.Builds
            .FirstOrDefaultAsync(
                build =>
                    build.RepositoryId == repositoryId &&
                    build.ExternalBuildId == externalBuildId,
                cancellationToken);

    /// <summary>
    /// Deletes the build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>true</c> when the build existed and was deleted; otherwise <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(
        int buildId,
        CancellationToken cancellationToken = default)
    {
        var build = await GetAsync(
            buildId,
            cancellationToken);

        if (build is null)
            return false;

        context.Builds.Remove(build);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}