using Build_Test_Dashboard.Controllers;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Test.Support;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="BuildsController"/> class in the build and test dashboard.
/// </summary>
public class BuildsControllerTests
{
    /// <summary>
    /// Test that Get returns a build.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsBuild()
    {
        var buildStore = AllBuilds;
        var buildService = new BuildService(buildStore);

        var controller = new BuildsController(buildService);
        var result = await controller.Get(1, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
    }

    /// <summary>
    /// Test that GetTests returns a list of Test Runs.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsTests()
    {
        var buildStore = AllBuilds;
        var buildService = new BuildService(buildStore);

        var controller = new BuildsController(buildService);
        var result = await controller.GetTests(1, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var tests = Assert.IsType<IEnumerable<TestRun>>(okResult.Value, exactMatch: false);

        Assert.NotEmpty(tests);
    }

    /// <summary>
    /// Test that Get returns a build.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsNoBuild()
    {
        var buildStore = AllBuilds;
        var buildService = new BuildService(buildStore);

        var controller = new BuildsController(buildService);
        var result = await controller.Get(3, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
    }

    /// <summary>
    /// Test that GetTests returns a list of Test Runs.
    /// </summary>
    [Fact]
    public async Task Get_ReturnsNoTests()
    {
        var buildStore = AllBuilds;
        var buildService = new BuildService(buildStore);

        var controller = new BuildsController(buildService);
        var result = await controller.GetTests(3, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// All builds
    /// </summary>
    readonly IBuildStore AllBuilds = new FakeBuildStore
    {
        Builds =
        [
            new Build
        {
            Id = 1,
            SourceRepositoryId = 1,
            BuildNumber = "100",
            Branch = "main",
            Commit = "abc123",
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Id = 1,
                    BuildId = 1,
                    Total = 100,
                    Passed = 98,
                    Failed = 1,
                    Skipped = 1,
                    Duration = TimeSpan.FromSeconds(42)
                }
            ]
        },
        new Build
        {
            Id = 2,
            SourceRepositoryId = 1,
            BuildNumber = "101",
            Branch = "main",
            Commit = "def456",
            Status = "Succeeded",
            TestRuns =
            [
                new TestRun
                {
                    Id = 2,
                    BuildId = 2,
                    Total = 150,
                    Passed = 150,
                    Failed = 0,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(55)
                },
                new TestRun
                {
                    Id = 3,
                    BuildId = 2,
                    Total = 25,
                    Passed = 24,
                    Failed = 1,
                    Skipped = 0,
                    Duration = TimeSpan.FromSeconds(8)
                }
            ]
        }
        ]
    };
}