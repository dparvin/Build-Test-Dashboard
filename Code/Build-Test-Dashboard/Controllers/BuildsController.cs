using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Controllers;

/// <summary>
/// Represents the controller for managing builds in the build and test dashboard.
/// </summary>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class BuildsController(IBuildService buildService) : ControllerBase
{
    /// <summary>
    /// The build service
    /// </summary>
    private readonly IBuildService buildService = buildService;

    /// <summary>
    /// Gets the specified build.
    /// </summary>
    /// <param name="id">The build.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Build? Get(int id)
    {
        return buildService.GetAsync(id);
    }

    /// <summary>
    /// Gets the tests that were run on a build.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpGet("{id}/tests")]
    public IEnumerable<TestRun> GetTests(int id)
    {
        return
        [
            new TestRun
            {
                Id = 1,
                BuildId = id,
                Total = 100,
                Passed = 98,
                Failed = 1,
                Skipped = 1,
                Duration = TimeSpan.FromSeconds(42)
            }
        ];
    }
}