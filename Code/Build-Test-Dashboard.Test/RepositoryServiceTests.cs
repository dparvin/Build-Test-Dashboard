using Build_Test_Dashboard.Exceptions;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Build_Test_Dashboard.Test;

public class RepositoryServiceTests
{
    private readonly TestWebApplicationFactory factory;

    public RepositoryServiceTests()
    {
        factory = new TestWebApplicationFactory();
    }

    [Fact]
    public async Task SaveAsync_CredentialStoreFails_NewRepository_RollsBackRepository()
    {
        factory.RepositoryStore.FindResult = null;

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

    [Fact]
    public async Task SaveAsync_CredentialStoreFails_ExistingRepository_RollsBackRepository()
    {
        // Arrange
        var previousRepository = new Repository
        {
            Id = 42,
            Name = "Original Repository",
            Provider = "GitHub",
            Owner = "OriginalOwner",
            Project = "OriginalProject",
            RepositoryName = "OriginalRepository"
        };

        factory.RepositoryStore.FindResult = previousRepository;

        factory.CredentialStore.StoreException =
            new InvalidOperationException("Credential store failed.");

        var request = new CreateRepositoryRequest
        {
            Repository = new Repository
            {
                Name = "Updated Repository",
                Provider = "GitHub",
                Owner = "UpdatedOwner",
                Project = "UpdatedProject",
                RepositoryName = "UpdatedRepository"
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

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => repositoryService.SaveAsync(
                request,
                TestContext.Current.CancellationToken));

        // Assert
        Assert.Equal(
            "Credential store failed.",
            exception.Message);

        Assert.Equal(42, request.Repository.Id);

        Assert.Equal(
            previousRepository,
            factory.RepositoryStore.StoredRepository);
    }

    [Fact]
    public async Task SaveAsync_CredentialStoreFails_NewRepository_RollsBackRepositoryFails()
    {
        factory.RepositoryStore.FindResult = null;

        factory.RepositoryStore.DeleteException =
            new InvalidOperationException("Repository store delete failed.");

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

        var exception = await Assert.ThrowsAsync<RepositorySaveException>(
            () => repositoryService.SaveAsync(
                request,
                TestContext.Current.CancellationToken));

        Assert.Equal(1, factory.RepositoryStore.StoreCallCount);

        Assert.NotNull(factory.RepositoryStore.StoredRepository);

        Assert.Equal(
            "Saving the repository credentials failed, and the repository rollback also failed.",
            exception.Message);

        Assert.Equal(
            "Credential store failed.",
            exception.InnerException?.Message);

        Assert.Equal(
            "Repository store delete failed.",
            exception.RollbackException?.Message);

        Assert.Equal(1, factory.RepositoryStore.DeleteCallCount);
    }
}
