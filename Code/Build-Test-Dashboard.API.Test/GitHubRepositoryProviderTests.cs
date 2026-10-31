using Build_Test_Dashboard.API.Providers;
using Build_Test_Dashboard.API.Services;
using Build_Test_Dashboard.API.Test.Support;
using Build_Test_Dashboard.Contracts.Enums;
using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Contracts.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.API.Test;

/// <summary>
/// Represents the unit tests for the <see cref="GitHubRepositoryProvider"/> class.
/// </summary>
public class GitHubRepositoryProviderTests
{
    /// <summary>
    /// The factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubRepositoryProviderTests"/> class.
    /// </summary>
    public GitHubRepositoryProviderTests()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// Validates the owner asynchronous task canceled without cancellation returns unknown error.
    /// </summary>
    [Fact]
    public async Task ValidateOwnerAsync_TaskCanceledWithoutCancellation_ReturnsUnknownError()
    {
        factory.GitHubHandler.SendFunc = _ =>
            throw new TaskCanceledException("Request timed out.");

        var request = new CreateRepositoryRequest
        {
            Repository = new Repository
            {
                Name = "Test Repository",
                Provider = "GitHub",
                Owner = "TestOwner",
                Project = "TestProject",
                RepositoryName = "TestRepository"
            },
            Credential = new Credential
            {
                AuthenticationType = "PersonalAccessToken",
                Secret = "test-secret"
            }
        };

        using var scope = factory.Services.CreateScope();

        var repositoryService =
            scope.ServiceProvider.GetRequiredService<RepositoryService>();

        var result = await repositoryService.ValidateRepositoryAsync(
            request,
            CancellationToken.None);

        Assert.Equal(
            RepositoryValidationResult.UnknownError,
            result);
    }
}