using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// Represents a store for managing repositories in the build and test dashboard.
/// </summary>
/// <seealso cref="IRepositoryStore" />
public class RepositoryStore(
    DashboardDbContext context) : IRepositoryStore
{
    /// <summary>
    /// The database context
    /// </summary>
    readonly DashboardDbContext _context = context;

    /// <summary>
    /// Stores the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Repository> StoreAsync(
        Repository repository,
        CancellationToken cancellationToken)
    {
        _context.Repositories.Add(repository);

        await _context.SaveChangesAsync(cancellationToken);

        return repository;
    }

    /// <summary>
    /// Gets all of the repositories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Repository>> GetAllAsync(
        CancellationToken cancellationToken) =>
        // retrieve all repositories from the database
        await _context.Repositories
            .OrderBy(r => r.Id)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Gets the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository.
    /// </returns>
    public async Task<Repository?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken) =>
        // Use the FindAsync method to retrieve the repository by its identifier
        await _context.Repositories
                .FindAsync([repositoryId], cancellationToken);

    /// <summary>
    /// Finds the repository asynchronously.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///   <c>null</c> if not found, otherwise the repository.
    /// </returns>
    public async Task<Repository?> FindAsync(
        Repository repository,
        CancellationToken cancellationToken = default) =>
        // Use LINQ to query the database for a repository that matches the provided repository's properties
        await _context.Repositories
            .FirstOrDefaultAsync(
                r =>
                    r.Provider == repository.Provider &&
                    r.Owner == repository.Owner &&
                    r.Project == repository.Project &&
                    r.RepositoryName == repository.RepositoryName,
                cancellationToken);

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken)
    {
        // Check if the repository exists
        var repository = await GetAsync(repositoryId, cancellationToken);

        // If the repository does not exist, return false
        if (repository is null)
        {
            return false;
        }

        // Remove the repository from the database
        _context.Repositories.Remove(repository);

        // Save changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}