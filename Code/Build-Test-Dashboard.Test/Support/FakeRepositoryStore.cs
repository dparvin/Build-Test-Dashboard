using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// A fake of the RepositoryStore
/// </summary>
/// <seealso cref="IRepositoryStore" />
public class FakeRepositoryStore : IRepositoryStore
{
    /// <summary>
    /// Gets or sets the find result.
    /// </summary>
    /// <value>
    /// The find result.
    /// </value>
    public Repository? FindResult { get; set; }

    /// <summary>
    /// Gets or sets the store exception.
    /// </summary>
    /// <value>
    /// The store exception.
    /// </value>
    public Exception? StoreException { get; set; } = null;

    /// <summary>
    /// Gets or sets the delete exception.
    /// </summary>
    /// <value>
    /// The delete exception.
    /// </value>
    public Exception? DeleteException { get; set; } = null;

    /// <summary>
    /// Gets the stored repository.
    /// </summary>
    /// <value>
    /// The stored repository.
    /// </value>
    public Repository? StoredRepository { get; private set; }

    /// <summary>
    /// Gets all the stored repositories.
    /// </summary>
    /// <value>
    /// All stored repositories.
    /// </value>
    public IEnumerable<Repository>? AllStoredRepositories { get; set; } = null;

    /// <summary>
    /// Gets the find call count.
    /// </summary>
    /// <value>
    /// The find call count.
    /// </value>
    public int FindCallCount { get; private set; }
    /// <summary>
    /// Gets the store call count.
    /// </summary>
    /// <value>
    /// The store call count.
    /// </value>
    public int StoreCallCount { get; private set; }
    /// <summary>
    /// Gets the delete call count.
    /// </summary>
    /// <value>
    /// The delete call count.
    /// </value>
    public int DeleteCallCount { get; private set; }

    /// <summary>
    /// Finds the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository.
    /// </returns>
    public Task<Repository?> FindAsync(
        Repository repository,
        CancellationToken cancellationToken = default)
    {
        FindCallCount++;

        return Task.FromResult(FindResult);
    }

    /// <summary>
    /// Gets all of the repositories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<IEnumerable<Repository>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AllStoredRepositories ?? []);
    }

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository.
    /// </returns>
    public Task<Repository?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        if (StoredRepository is not null)
        {
            return Task.FromResult(
                StoredRepository.Id == repositoryId
                    ? StoredRepository
                    : null);
        }

        if (AllStoredRepositories is not null)
        {
            return Task.FromResult(
                AllStoredRepositories.FirstOrDefault(
                    repository => repository.Id == repositoryId));
        }

        return Task.FromResult<Repository?>(null);
    }

    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<Repository> StoreAsync(
        Repository repository,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;

        if (StoreException != null)
            throw StoreException;

        if (repository.Id == 0)
            repository.Id = 1;

        StoredRepository = repository;

        return Task.FromResult(repository);
    }

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<bool> DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        DeleteCallCount++;

        if (DeleteException != null)
            throw DeleteException;

        if (StoredRepository?.Id != repositoryId)
            return Task.FromResult(false);

        StoredRepository = null;

        return Task.FromResult(true);
    }
}