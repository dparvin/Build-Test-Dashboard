using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// Represents a store for managing repositories in the build and test dashboard.
/// </summary>
public class RepositoryStore : IRepositoryStore
{
    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<Repository> StoreAsync(Repository repository, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<Repository?> GetAsync(int repositoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Finds the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository identifier.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<Repository?> FindAsync(Repository repository, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<bool> DeleteAsync(int repositoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}