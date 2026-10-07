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
    public async Task<Build> SaveAsync(
        Build build,
        CancellationToken cancellationToken = default) =>
        await buildStore.StoreAsync(build, cancellationToken);

    /// <summary>
    /// Gets a build asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
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
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the builds for a repository.
    /// </returns>
    public async Task<IEnumerable<Build>> GetAllAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default) =>
        await buildStore.GetAllAsync(
            buildConnectionId,
            cancellationToken);

    /// <summary>
    /// Gets the by source repository asynchronous.
    /// </summary>
    /// <param name="sourceRepositoryId">The source repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Build>> GetBySourceRepositoryAsync(
        int sourceRepositoryId,
        CancellationToken cancellationToken = default) =>
        await buildStore.GetBySourceRepositoryAsync(
            sourceRepositoryId,
            cancellationToken);

    /// <summary>
    /// Finds the build asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="externalBuildId">The external build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    public async Task<Build?> FindAsync(
        int buildConnectionId,
        string externalBuildId,
        CancellationToken cancellationToken = default) =>
        await buildStore.FindAsync(
            buildConnectionId,
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
    public async Task<bool> DeleteAsync(
        int buildId,
        CancellationToken cancellationToken = default) =>
        await buildStore.DeleteAsync(
            buildId,
            cancellationToken);
}