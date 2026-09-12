using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Represents a store for managing credentials in the build and test dashboard.
/// </summary>
public interface ICredentialStore
{
    /// <summary>
    /// Stores the credential asynchronously.
    /// </summary>
    /// <param name="credential">The credential.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task StoreAsync(RepositoryCredential credential, CancellationToken cancellationToken = default);
    /// <summary>
    /// Gets the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<RepositoryCredential?> GetAsync(int repositoryId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task DeleteAsync(int repositoryId, CancellationToken cancellationToken = default);
}