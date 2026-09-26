using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Interface for the Build Store
/// </summary>
public interface IBuildStore
{
    /// <summary>
    /// Stores the build asynchronously.
    /// </summary>
    /// <param name="build">The build.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The stored build.</returns>
    Task<Build> StoreAsync(
        Build build,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    Task<Build?> GetAsync(
        int buildId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all builds for a repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>All of the builds for a repository.</returns>
    Task<IEnumerable<Build>> GetAllAsync(
        int repositoryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the build existed and was deleted; otherwise <c>false</c>.
    /// </returns>
    Task<bool> DeleteAsync(
        int buildId,
        CancellationToken cancellationToken = default);
}