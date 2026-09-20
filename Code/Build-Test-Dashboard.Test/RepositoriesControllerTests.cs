using Build_Test_Dashboard.Controllers;
using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Stores;
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
        var repositoryStore = new RepositoryStore();
        var credentialStore = new WindowsCredentialStore();
        var providers = Array.Empty<IRepositoryProvider>();

        var service = new RepositoryService(providers, repositoryStore, credentialStore);

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
        var repositoryStore = new RepositoryStore();
        var credentialStore = new WindowsCredentialStore();
        var providers = Array.Empty<IRepositoryProvider>();

        var service = new RepositoryService(providers, repositoryStore, credentialStore);

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
        var repositoryStore = new RepositoryStore();
        var credentialStore = new WindowsCredentialStore();
        var providers = Array.Empty<IRepositoryProvider>();

        var service = new RepositoryService(providers, repositoryStore, credentialStore);

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
        factory.RepositoryServiceState.UseRealService = true;

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
    [InlineData("GitHub", HttpStatusCode.OK, HttpStatusCode.OK, "Test Repository")]
    [InlineData("AzureDevOps", HttpStatusCode.OK, HttpStatusCode.OK, "Test Repository")]
    [InlineData("GitHub", HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized, "")]
    [InlineData("AzureDevOps", HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized, "")]
    [InlineData("GitHub", HttpStatusCode.NotFound, HttpStatusCode.NotFound, "")]
    [InlineData("AzureDevOps", HttpStatusCode.NotFound, HttpStatusCode.NotFound, "")]
    [InlineData("GitHub", HttpStatusCode.NotAcceptable, HttpStatusCode.BadGateway, "")]
    [InlineData("AzureDevOps", HttpStatusCode.NotAcceptable, HttpStatusCode.BadGateway, "")]
    public async Task Post_APICall_Creates_Repository_Connection(
        string provider,
        HttpStatusCode statusCode,
        HttpStatusCode expectedStatusCode,
        string expectedRepositoryName)
    {
        factory.RepositoryServiceState.UseRealService = true;
        if (provider == "GitHub")
        {
            factory.GitHubHandler.StatusCode = statusCode;
            factory.GitHubHandler.SendFunc = GitHubSendFunc;
        }
        else if (provider == "AzureDevOps")
        {
            factory.AzureDevOpsHandler.StatusCode = statusCode;
            //factory.AzureDevOpsHandler.SendFunc = AzureDevOpsSendFunc;
        }


        var client = factory.CreateClient();

        var request = new CreateRepositoryRequest
        {
            Repository = new Repository
            {
                Name = "Test Repository",
                Provider = provider,
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

        factory.RepositoryStore.FindResult = null;
        factory.CredentialStore.StoredCredential = null;


        var response = await client.PostAsJsonAsync("/api/repositories", request, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var createdRepository =
                await response.Content.ReadFromJsonAsync<Repository>(cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(createdRepository);
            Assert.Equal(expectedRepositoryName, createdRepository.Name);
        }

        Assert.Equal((provider == "GitHub" ? 2 : 0), factory.GitHubHandler.CallCount);
        Assert.Equal((provider == "AzureDevOps" ? 1 : 0), factory.AzureDevOpsHandler.CallCount);
    }

    private HttpResponseMessage GitHubSendFunc(HttpRequestMessage request)
    {
        var path = request.RequestUri?.AbsolutePath;

        if (path == "/users/TestOwner")
            return new HttpResponseMessage(HttpStatusCode.OK);
        if (path == "/repos/TestOwner/TestRepository")
            return new HttpResponseMessage(factory.GitHubHandler.StatusCode);
        return new HttpResponseMessage(HttpStatusCode.NotFound);
    }

    private HttpResponseMessage AzureDevOpsSendFunc(HttpRequestMessage arg)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Posts the API call fails to creates repository.
    /// </summary>
    [Fact()]
    public async Task Post_APICall_Fails_To_Creates_Repository()
    {
        var client = factory.CreateClient();
        factory.RepositoryServiceState.UseRealService = true;
        var repository = new Repository
        {
            Name = "Test Repository",
            Provider = "Someplace",
            Owner = "ATestOwner",
            RepositoryName = "ATestRepository"
        };

        var response = await client.PostAsJsonAsync("/api/repositories", repository, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Theory()]
    [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.NotFound, HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.NotAcceptable, HttpStatusCode.BadGateway)]
    public async Task Post_APICall_Creates_RepositoryOwner_Connection(
        HttpStatusCode statusCode,
        HttpStatusCode expectedStatusCode)
    {
        factory.RepositoryServiceState.UseRealService = true;
        factory.GitHubHandler.SendFunc = request =>
        {
            var path = request.RequestUri?.AbsolutePath;

            if (path == "/users/TestOwner")
                return new HttpResponseMessage(statusCode);

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        };


        var client = factory.CreateClient();

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

        factory.RepositoryStore.FindResult = null;
        factory.CredentialStore.StoredCredential = null;


        var response = await client.PostAsJsonAsync("/api/repositories", request, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);

        Assert.Equal(1, factory.GitHubHandler.CallCount);
        Assert.Equal(0, factory.AzureDevOpsHandler.CallCount);
    }

    /// <summary>
    /// Posts the API call creates repository.
    /// </summary>
    [Theory()]
    [InlineData(RepositoryValidationResult.InvalidCredentials, HttpStatusCode.Unauthorized, null)]
    [InlineData(RepositoryValidationResult.OwnerNotFound, HttpStatusCode.NotFound, "Repository owner was not found.")]
    [InlineData(RepositoryValidationResult.ProjectNotFound, HttpStatusCode.NotFound, "Repository project was not found.")]
    [InlineData(RepositoryValidationResult.RepositoryNotFound, HttpStatusCode.NotFound, "Repository was not found.")]
    [InlineData(RepositoryValidationResult.UnknownError, HttpStatusCode.BadGateway, null)]
    [InlineData((RepositoryValidationResult)999, HttpStatusCode.InternalServerError, null)]
    public async Task Post_APICall_Failed_GitHub_Validation(
        RepositoryValidationResult statusCode,
        HttpStatusCode expectedStatusCode,
        string? expectedMessage)
    {

        var client = factory.CreateClient();
        factory.RepositoryServiceState.UseRealService = false;
        factory.RepositoryServiceState.ValidationResult = statusCode;

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

        factory.RepositoryStore.FindResult = null;
        factory.CredentialStore.StoredCredential = null;

        var response = await client.PostAsJsonAsync("/api/repositories", request, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);

        var message = await response.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken);

        Output($"Response message: {message}");

        if (expectedMessage != null)
            Assert.Equal(expectedMessage, message);
    }

    /// <summary>
    /// Outputs the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    private static void Output(string message)
    {
        TestContext.Current.TestOutputHelper?.WriteLine(message);
    }
}