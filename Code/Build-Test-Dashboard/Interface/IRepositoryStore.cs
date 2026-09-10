using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

public interface IRepositoryStore
{
    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <returns></returns>
    Task StoreAsync(Repository repository);
    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    Task<Repository?> GetAsync(int repositoryId);
    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(int repositoryId);
}