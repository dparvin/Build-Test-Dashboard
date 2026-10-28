using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Controllers;
using Build_Test_Dashboard.Interfaces;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Build_Test_Dashboard.Test;

public class BuildConnectionControllerTests
{
    /// <summary>
    /// The test Web Application factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildConnectionControllerTests"/> class.
    /// </summary>
    public BuildConnectionControllerTests() =>
        factory = new TestWebApplicationFactory();

    /// <summary>
    /// Gets the returns build connection.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsBuildConnection()
    {
        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.Get(1, TestContext.Current.CancellationToken);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var buildConnection = Assert.IsType<BuildConnection>(okResult.Value);

        Assert.Equal(1, buildConnection.Id);
    }

    /// <summary>
    /// Gets the returns nothing.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsNothing()
    {
        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.Get(3, TestContext.Current.CancellationToken);

        Assert.NotNull(result.Result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Gets all returns build connections.
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsBuildConnections()
    {
        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.GetAll(TestContext.Current.CancellationToken);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var buildConnections = Assert.IsType<IEnumerable<BuildConnection>>(okResult.Value, false);

        Assert.Equal(1, buildConnections.ElementAt(0).Id);
        Assert.Equal(2, buildConnections.ElementAt(1).Id);
    }

    /// <summary>
    /// Posts this instance.
    /// </summary>
    [Fact]
    public async Task Post()
    {
        var buildConnection = new BuildConnection
        {
            Id = 0,
            Provider = "GitHub",
            Name = "NewBuildConnection"
        };

        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.Post(buildConnection, TestContext.Current.CancellationToken);
        Assert.IsType<ActionResult<BuildConnection>>(result);

        var buildConnections = (result.Result as OkObjectResult)?.Value as BuildConnection;
        Assert.Equal("NewBuildConnection", buildConnections?.Name);
        Assert.Equal("GitHub", buildConnections?.Provider);
        Assert.Equal(3, buildConnections?.Id);
    }

    [Fact]
    public async Task Delete()
    {
        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.Delete(1, TestContext.Current.CancellationToken);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_Missing()
    {
        var store = new FakeBuildConnectionStore();
        var service = new BuildConnectionService(store);

        var buildConnectionService = new FakeBuildConnectionService(service, state);
        var controller = new BuildConnectionsController(buildConnectionService);
        var result = await controller.Delete(3, TestContext.Current.CancellationToken);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Get_APICall_Returns_BuildConnections()
    {
        factory.BuildConnectionServiceState.UseRealService = false;
        factory.BuildConnectionServiceState.BuildConnectionList = state.BuildConnectionList;
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/buildconnections", TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        var buildConnections =
            await response.Content.ReadFromJsonAsync<List<BuildConnection>>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(buildConnections);
        Assert.NotEmpty(buildConnections);
    }

    [Theory]
    [InlineData(3, HttpStatusCode.NotFound)]
    [InlineData(2, HttpStatusCode.OK)]
    public async Task Get_APICall_Returns_BuildConnection(
        int id,
        HttpStatusCode expectedStatusCode)
    {
        factory.BuildConnectionServiceState.UseRealService = false;
        factory.BuildConnectionServiceState.BuildConnectionList = state.BuildConnectionList;
        var client = factory.CreateClient();

        var response = await client.GetAsync($"/api/buildconnections/{id}", TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var buildConnection =
                await response.Content.ReadFromJsonAsync<BuildConnection>(
                    cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(buildConnection);
            Assert.Equal(id, buildConnection.Id);
        }
    }

    [Fact]
    public async Task Post_APICall()
    {
        var buildConnection = new BuildConnection
        {
            Id = 0,
            Provider = "GitHub",
            Name = "NewBuildConnection"
        };

        var client = factory.CreateClient();
        factory.BuildConnectionServiceState.UseRealService = false;
        factory.BuildConnectionServiceState.BuildConnectionList = state.BuildConnectionList;

        var response = await client.PostAsync("/api/buildconnections", JsonContent.Create(buildConnection), TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdBuildConnection =
            await response.Content.ReadFromJsonAsync<BuildConnection>(
                cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(createdBuildConnection);
        Assert.Equal("NewBuildConnection", createdBuildConnection.Name);
        Assert.Equal("GitHub", createdBuildConnection.Provider);
        Assert.Equal(3, createdBuildConnection.Id);
    }

    /// <summary>
    /// Deletes the API call.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="expectedStatusCode">The expected status code.</param>
    [Theory]
    [InlineData(3, HttpStatusCode.NotFound)]
    [InlineData(2, HttpStatusCode.NoContent)]
    public async Task Delete_APICall(
        int repositoryId,
        HttpStatusCode expectedStatusCode)
    {
        var client = factory.CreateClient();
        factory.BuildConnectionServiceState.UseRealService = false;
        factory.BuildConnectionServiceState.BuildConnectionList = state.BuildConnectionList;

        var response = await client.DeleteAsync($"/api/buildconnections/{repositoryId}", TestContext.Current.CancellationToken);

        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    /// <summary>
    /// The state
    /// </summary>
    private readonly FakeBuildConnectionServiceState state = new()
    {
        UseRealService = false,
        BuildConnectionList =
        [
            new BuildConnection
                {
                    Id = 1,
                    Provider = "GitHub",
                    Name = "EventLogHelper"
                },
                new BuildConnection
                {
                    Id = 2,
                    Provider = "GitHub",
                    Name = "PropertyGridHelpers"
                }

        ]
    };

    /// <summary>
    /// All builds
    /// </summary>
    private readonly IBuildConnectionStore AllBuildConnections = new FakeBuildConnectionStore
    {
        BuildConnections =
        [
            new BuildConnection
            {
                Id = 1,
                Provider = "GitHub",
                Name = "EventLogHelper"
            },
            new BuildConnection
            {
                Id = 2,
                Provider = "GitHub",
                Name = "PropertyGridHelpers"
            }
        ]
    };
}
