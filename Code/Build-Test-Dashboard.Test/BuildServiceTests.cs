using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Tests for <see cref="BuildService"/>
/// </summary>
public class BuildServiceTests
{
    /// <summary>
    /// The factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildServiceTests"/> class.
    /// </summary>
    public BuildServiceTests()
    {
        factory = new TestWebApplicationFactory();
    }

    [Fact]
    public async Task SaveAsync()
    {
        factory.BuildStore.FindResult = null;

        factory.CredentialStore.StoreException =
            new InvalidOperationException("Credential store failed.");

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
            Credential = new RepositoryCredential
            {
                AuthenticationType = "PersonalAccessToken",
                Secret = "test-secret"
            }
        };

        using var scope = factory.Services.CreateScope();
        var repositoryService =
            scope.ServiceProvider.GetRequiredService<RepositoryService>();

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => repositoryService.SaveAsync(
                request,
                TestContext.Current.CancellationToken));

        Assert.Equal(
            "Credential store failed.",
            exception.Message);

        Assert.Equal(1, factory.RepositoryStore.DeleteCallCount);
    }

}
