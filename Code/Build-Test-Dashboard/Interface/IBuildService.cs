using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Interface for BuildService
/// </summary>
public interface IBuildService
{
    /// <summary>
    /// Gets the by repository identifier asynchronous.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Build>> GetAllAsync(
        int repositoryId,
        CancellationToken cancellationToken = default);
}