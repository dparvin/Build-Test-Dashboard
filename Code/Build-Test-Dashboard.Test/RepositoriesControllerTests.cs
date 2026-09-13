using Build_Test_Dashboard.Controllers;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using System.Net;
using System.Net.Http.Json;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="RepositoriesController"/> class.
/// </summary>
public class RepositoriesControllerTests
{
    private readonly TestWebApplicationFactory factory;

    public RepositoriesControllerTests()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// The Get call returns a list of repositories.
    /// </summary>
    [Fact]
    public void Get_ReturnsRepositories()
    {
        var providers = Array.Empty<IRepositoryProvider>();

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
        var providers = Array.Empty<IRepositoryProvider>();

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
        var providers = Array.Empty<IRepositoryProvider>();

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
    [InlineData("GitHub", HttpStatusCode.OK, HttpStatusCode.Created, "Test Repository")]
    [InlineData("AzureDevOps", HttpStatusCode.OK, HttpStatusCode.Created, "Test Repository")]
    [InlineData("GitHub", HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized, "")]
    [InlineData("AzureDevOps", HttpStatusCode.Unauthorized, HttpStatusCode.Created, "Test Repository")]
    public async Task Post_APICall_Creates_Repository(
        string provider,
        HttpStatusCode statusCode,
        HttpStatusCode expectedStatusCode,
        string expectedRepositoryName)
    {
        factory.GitHubHandler.Response = new HttpResponseMessage(statusCode);
        var client = factory.CreateClient();

        var request = new CreateRepositoryRequest
        {
            Repository = new Repository
            {
                Name = "Test Repository",
                Provider = provider,
                Owner = "TestOwner",
                RepositoryName = "TestRepository"
            },
            Credential = new RepositoryCredential
            {
                AuthenticationType = "PersonalAccessToken",
                Secret = "test-secret"
            }
        };


        var response = await client.PostAsJsonAsync("/api/repositories", request, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);

        var createdRepository =
            await response.Content.ReadFromJsonAsync<Repository>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(createdRepository);
        Assert.Equal(expectedRepositoryName, createdRepository.Name);
    }

    /// <summary>
    /// Posts the API call fails to creates repository.
    /// </summary>
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