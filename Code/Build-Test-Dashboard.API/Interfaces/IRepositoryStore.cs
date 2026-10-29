using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.API.Interfaces;

/// <summary>
/// Represents a store for repository operations in the build and test dashboard.
/// </summary>
public interface IRepositoryStore
{
    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<Repository> StoreAsync(
        Repository repository,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all of the repositories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Repository>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="id">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>null</c> if not found, otherwise the repository.</returns>
    Task<Repository?> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>null</c> if not found, otherwise the repository.</returns>
    Task<Repository?> FindAsync(
        Repository repository,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="id">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}