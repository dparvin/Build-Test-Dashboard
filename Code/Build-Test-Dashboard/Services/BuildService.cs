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
}