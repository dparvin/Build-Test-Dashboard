using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Results;

namespace Build_Test_Dashboard.Interface;

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
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<Repository?> GetAsync(
        int repositoryId,
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
}