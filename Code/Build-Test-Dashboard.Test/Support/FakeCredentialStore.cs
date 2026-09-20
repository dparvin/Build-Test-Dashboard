using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support;

public class FakeCredentialStore : ICredentialStore
{
    public RepositoryCredential? StoredCredential { get; set; }

    public int StoreCallCount { get; private set; }

    /// <summary>
    /// Gets or sets the store exception.
    /// </summary>
    /// <value>
    /// The store exception.
    /// </value>
    public Exception? StoreException { get; set; }

    /// <summary>
    /// Stores the credential asynchronously.
    /// </summary>
    /// <param name="credential">The credential.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task StoreAsync(
        RepositoryCredential credential,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;
        StoredCredential = credential;

        if (StoreException != null)
            throw StoreException;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<RepositoryCredential?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}