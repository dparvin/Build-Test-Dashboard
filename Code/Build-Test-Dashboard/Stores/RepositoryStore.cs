using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// Represents a store for managing repositories in the build and test dashboard.
/// </summary>
public class RepositoryStore : IRepositoryStore
{
    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task DeleteAsync(int repositoryId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task<Repository?> GetAsync(int repositoryId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Task StoreAsync(Repository repository)
    {
        throw new NotImplementedException();
    }
}