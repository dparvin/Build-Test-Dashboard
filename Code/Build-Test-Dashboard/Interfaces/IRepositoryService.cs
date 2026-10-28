using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Contracts.Requests;
using Build_Test_Dashboard.Contracts.Results;
using Build_Test_Dashboard.Contracts.Enums;

namespace Build_Test_Dashboard.Interfaces;

/// <summary>
/// Interface for the repository service
/// </summary>
public interface IRepositoryService
{
    /// <summary>
    /// Gets all of the repositories asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Repository>> GetAllAsync(
    CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the repository asynchronous.
    /// </summary>
    /// <param name="id">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<Repository?> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates the repository asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the Repository asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<SaveRepositoryResult> SaveAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="id">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}