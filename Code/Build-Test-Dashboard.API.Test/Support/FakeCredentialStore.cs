using Build_Test_Dashboard.API.Interfaces;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.API.Test.Support;

/// <summary>
/// Represents a fake implementation of ICredentialStore for testing purposes.
/// </summary>
/// <seealso cref="ICredentialStore" />
public class FakeCredentialStore : ICredentialStore
{
    /// <summary>
    /// Gets or sets the stored credential.
    /// </summary>
    /// <value>
    /// The stored credential.
    /// </value>
    public Credential? StoredCredential { get; set; }

    /// <summary>
    /// Gets the store call count.
    /// </summary>
    /// <value>
    /// The store call count.
    /// </value>
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