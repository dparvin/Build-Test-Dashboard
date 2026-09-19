using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
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
    /// Saves the asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<SaveRepositoryResult> SaveAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realRepositoryService.SaveAsync(request, cancellationToken);

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
}