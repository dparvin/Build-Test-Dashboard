using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Services;

/// <summary>
///  The build service, providing routines to service the build store
/// </summary>
/// <param name="buildStore">the build store to use to access the builds</param>
/// <seealso cref="IBuildService" />
public class BuildService(IBuildStore buildStore) : IBuildService
{
    /// <summary>
    /// The build store
    /// </summary>
    private readonly IBuildStore buildStore = buildStore;

    /// <summary>
    /// Stores the build asynchronously.
    /// </summary>
    /// <param name="build">The build.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The stored build.
    /// </returns>
    public Task<Build> SaveAsync(
        Build build,
        CancellationToken cancellationToken = default) =>
        buildStore.StoreAsync(build, cancellationToken);

    /// <summary>
    /// Gets a build asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    public async Task<Build?> GetAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        await buildStore.GetAsync(
            id,
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
        await buildStore.GetAllAsync(
            repositoryId,
            cancellationToken);

    /// <summary>
    /// Finds the build asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="externalBuildId">The external build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    public async Task<Build?> FindAsync(
        int repositoryId,
        string externalBuildId,
        CancellationToken cancellationToken = default) =>
        await buildStore.FindAsync(
            repositoryId,
            externalBuildId,
            cancellationToken);

    /// <summary>
    /// Deletes the build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>true</c> when the build existed and was deleted; otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> DeleteAsync(
        int buildId,
        CancellationToken cancellationToken = default) =>
        buildStore.DeleteAsync(
            buildId,
            cancellationToken);
}