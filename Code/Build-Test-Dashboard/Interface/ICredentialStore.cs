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
    /// <returns></returns>
    Task StoreAsync(RepositoryCredential credential);
    /// <summary>
    /// Gets the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    Task<RepositoryCredential?> GetAsync(int repositoryId);
    /// <summary>
    /// Deletes the credential asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(int repositoryId);
}