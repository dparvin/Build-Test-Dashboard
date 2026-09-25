using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

public interface IRepositoryStore
{
    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<Repository> StoreAsync(Repository repository, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all of the repositories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Repository>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>null</c> if not found, otherwise the repository.</returns>
    Task<Repository?> GetAsync(int repositoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>null</c> if not found, otherwise the repository.</returns>
    Task<Repository?> FindAsync(Repository repository, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int repositoryId, CancellationToken cancellationToken = default);
}