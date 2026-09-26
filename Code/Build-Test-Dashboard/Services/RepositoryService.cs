using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Exceptions;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Results;

namespace Build_Test_Dashboard.Services;

/// <summary>
/// Represents a service for managing repositories in the build and test dashboard.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RepositoryService"/> class.
/// </remarks>
/// <param name="repositoryProvider">The repository provider.</param>
public class RepositoryService(
    IEnumerable<IRepositoryProvider> providers,
    IRepositoryStore repositoryStore,
    ICredentialStore credentialStore) : IRepositoryService
{
    /// <summary>
    /// The repository provider
    /// </summary>
    private readonly IEnumerable<IRepositoryProvider> repositoryProvider = providers;
    /// <summary>
    /// The repository store
    /// </summary>
    private readonly IRepositoryStore repositoryStore = repositoryStore;
    /// <summary>
    /// The credential store
    /// </summary>
    private readonly ICredentialStore credentialStore = credentialStore;

    /// <summary>
    /// Validates the connection asynchronous.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        foreach (var provider in repositoryProvider)
        {
            if (string.Compare(request.Repository.Provider, provider.ProviderName, StringComparison.OrdinalIgnoreCase) == 0)
                return await provider.ValidateRepositoryAsync(request, cancellationToken);
        }

        throw new NotSupportedException(
            $"The repository provider '{request.Repository.Provider}' is not supported.");
    }

    /// <summary>
    /// Saves the repository asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="Build_Test_Dashboard.Exceptions.RepositorySaveException">Saving the repository credentials failed, and the repository rollback also failed.</exception>
    public async Task<SaveRepositoryResult> SaveAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        // Validate the repository before saving
        var validationResult =
            await ValidateRepositoryAsync(
                request,
                cancellationToken);

        // If the validation fails, return the validation result without saving
        if (validationResult != RepositoryValidationResult.Valid)
            return new SaveRepositoryResult
            {
                ValidationResult = validationResult
            };
        // Check if the repository already exists in the store

        var previousRepository =
            await repositoryStore.FindAsync(
                request.Repository,
                cancellationToken);

        // If the repository already exists, set the ID to update it instead of creating a new one
        if (previousRepository != null)
            request.Repository.Id = previousRepository.Id;

        // Save the repository to the store
        var repository =
            await repositoryStore.StoreAsync(
                request.Repository,
                cancellationToken);
        request.Credential.RepositoryId = repository.Id;

        // Save the credentials for the repository
        try
        {
            await credentialStore.StoreAsync(
                request.Credential,
                cancellationToken);
        }
        catch (Exception ex)
        {
            try
            {
                // Rollback the repository save operation if credential saving fails
                if (previousRepository != null)
                    await repositoryStore.StoreAsync(
                        previousRepository,
                        cancellationToken);
                else
                    await repositoryStore.DeleteAsync(
                        repository.Id,
                        cancellationToken);
            }
            catch (Exception rollbackException)
            {
                throw new RepositorySaveException(
                    "Saving the repository credentials failed, and the repository rollback also failed.",
                    ex,
                    rollbackException);
            }

            throw;
        }

        return new SaveRepositoryResult
        {
            ValidationResult = RepositoryValidationResult.Valid,
            Repository = repository
        };
    }

    /// <summary>
    /// Gets all of the repositories asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Repository>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await repositoryStore.GetAllAsync(cancellationToken);

    /// <summary>
    /// Gets the repository asynchronous.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Repository?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken = default) =>
        await repositoryStore.GetAsync(repositoryId, cancellationToken);
}