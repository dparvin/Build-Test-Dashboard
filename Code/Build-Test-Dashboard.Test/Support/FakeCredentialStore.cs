using Build_Test_Dashboard.Interfaces;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.Test.Support;

public class FakeCredentialStore : ICredentialStore
{
    public Credential? StoredCredential { get; set; }

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
        Credential credential,
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
    public Task<Credential?> GetAsync(
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