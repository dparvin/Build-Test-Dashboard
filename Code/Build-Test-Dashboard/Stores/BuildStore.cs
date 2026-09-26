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
    /// Stores the asynchronous.
    /// </summary>
    /// <param name="build">The build.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Build> StoreAsync(
        Build build,
        CancellationToken cancellationToken = default)
    {
        context.Builds.Add(build);

        await context.SaveChangesAsync(cancellationToken);

        return build;
    }

    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Build?> GetAsync(
        int buildId,
        CancellationToken cancellationToken = default) =>
        await context.Builds
            .FirstOrDefaultAsync(
                build =>
                    build.Id == buildId,
                cancellationToken);

    /// <summary>
    /// Gets the by repository identifier asynchronous.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Build>> GetAllAsync(
        int repositoryId,
        CancellationToken cancellationToken = default) =>
        await context.Builds
            .Where(build => build.RepositoryId == repositoryId)
            .OrderBy(build => build.Id)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
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