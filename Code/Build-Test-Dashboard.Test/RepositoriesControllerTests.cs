using Build_Test_Dashboard.Controllers;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Providers;
using Build_Test_Dashboard.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="RepositoriesController"/> class.
/// </summary>
public class RepositoriesControllerTests
{
    private readonly WebApplicationFactory<Program> factory;

    public RepositoriesControllerTests()
    {
        factory = new WebApplicationFactory<Program>();
    }

    /// <summary>
    /// The Get call returns a list of repositories.
    /// </summary>
    [Fact]
    public void Get_ReturnsRepositories()
    {
        var providers = new IRepositoryProvider[]
        {
            new GitHubRepositoryProvider(),
            new AzureDevOpsRepositoryProvider()
        };

        var service = new RepositoryService(providers);

        var controller = new RepositoriesController(service);
        var result = controller.Get();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    /// <summary>
    /// The Get call returns a repository.
    /// </summary>
    [Fact]
    public void Get_ReturnsRepository()
    {
        var providers = new IRepositoryProvider[]
        {
            new GitHubRepositoryProvider(),
            new AzureDevOpsRepositoryProvider()
        };

        var service = new RepositoryService(providers);

        var controller = new RepositoriesController(service);
        var result = controller.Get(1);

        Assert.NotNull(result);
    }

    /// <summary>
    /// The Get Builds call returns a repository builds.
    /// </summary>
    [Fact]
    public void Get_ReturnsRepositoryBuilds()
    {
        var providers = new IRepositoryProvider[]
        {
            new GitHubRepositoryProvider(),
            new AzureDevOpsRepositoryProvider()
        };

        var service = new RepositoryService(providers);

        var controller = new RepositoriesController(service);
        var result = controller.GetBuilds(1);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Gets the API call returns repositories.
    /// </summary>
    [Fact]
    public async Task Get_APICall_Returns_Repositories()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/repositories", TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        var repositories =
            await response.Content.ReadFromJsonAsync<List<Repository>>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(repositories);
        Assert.NotEmpty(repositories);
    }

    /// <summary>
    /// Posts the API call creates repository.
    /// </summary>
    [Theory()]
    [InlineData("GitHub")]
    [InlineData("AzureDevOps")]
    public async Task Post_APICall_Creates_Repository(string provider)
    {
        var client = factory.CreateClient();

        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = provider,
            Owner = "TestOwner",
            RepositoryName = "TestRepository"
        };

        var response = await client.PostAsJsonAsync("/api/repositories", repository, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdRepository =
            await response.Content.ReadFromJsonAsync<Repository>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(createdRepository);
        Assert.Equal("Test Repository", createdRepository.Name);
    }

    [Fact()]
    public async Task Post_APICall_Fails_To_Creates_Repository()
    {
        var client = factory.CreateClient();

        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "Someplace",
            Owner = "TestOwner",
            RepositoryName = "TestRepository"
        };

        var response = await client.PostAsJsonAsync("/api/repositories", repository, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
