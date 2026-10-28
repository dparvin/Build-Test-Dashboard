using Build_Test_Dashboard.Interfaces;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.Stores;

public class WindowsCredentialStore : ICredentialStore
{
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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
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
    public Task DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}