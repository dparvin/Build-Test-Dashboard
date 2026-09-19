using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Results;

namespace Build_Test_Dashboard.Interface;

public interface IRepositoryService
{
    Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default);

    Task<SaveRepositoryResult> SaveAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default);
}