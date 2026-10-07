using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Results;
using Build_Test_Dashboard.Services;

namespace Build_Test_Dashboard.Test.Support;

public class FakeRepositoryService(
    RepositoryService realRepositoryService,
    FakeRepositoryServiceState state) : IRepositoryService
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    public FakeRepositoryServiceState State { get; } = state;
    /// <summary>
    /// Gets the store call count.
    /// </summary>
    /// <value>
    /// The store call count.
    /// </value>
    public int StoreCallCount { get; private set; }
    /// <summary>
    /// Gets the delete call count.
    /// </summary>
    /// <value>
    /// The delete call count.
    /// </value>
    public int DeleteCallCount { get; private set; }

    /// <summary>
    /// Gets or sets the store exception.
    /// </summary>
    /// <value>
    /// The store exception.
    /// </value>
    public Exception? StoreException { get; set; } = null;

    /// <summary>
    /// Gets or sets the delete exception.
    /// </summary>
    /// <value>
    /// The delete exception.
    /// </value>
    public Exception? DeleteException { get; set; } = null;

    /// <summary>
    /// Gets the stored repository.
    /// </summary>
    /// <value>
    /// The stored repository.
    /// </value>
    public Repository? StoredRepository { get; set; } = null;

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<SaveRepositoryResult> SaveAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;

        if (State.UseRealService)
            return realRepositoryService.SaveAsync(request, cancellationToken);

        if (StoreException != null)
            throw StoreException;

        return Task.FromResult(
            new SaveRepositoryResult
            {
                ValidationResult = State.ValidationResult
            });
    }

    /// <summary>
    /// Validates the repository asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realRepositoryService.ValidateRepositoryAsync(
                request,
                cancellationToken);

        return Task.FromResult(State.ValidationResult);
    }

    /// <summary>
    /// Gets all of the repositories asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Repository>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realRepositoryService.GetAllAsync(
                cancellationToken);

        IEnumerable<Repository> list;

        list = State.RepositoryList ?? [];

        return Task.FromResult(list);
    }

    /// <summary>
    /// Gets the repository asynchronous.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Repository?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realRepositoryService.GetAsync(
                repositoryId,
                cancellationToken);

        IEnumerable<Repository> list;

        list = State.RepositoryList ?? [];

        return Task.FromResult(list.FirstOrDefault(repository => repository.Id == repositoryId));
    }

    /// <summary>
    /// Deletes the repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<bool> DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        DeleteCallCount++;

        if (DeleteException != null)
            throw DeleteException;

        if (StoredRepository?.Id != repositoryId)
            return Task.FromResult(false);

        StoredRepository = null;

        return Task.FromResult(true);
    }
}