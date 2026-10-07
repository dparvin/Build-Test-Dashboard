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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Build>> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var build = await buildService.GetAsync(
            id,
            cancellationToken);

        if (build is null)
            return NotFound();

        return Ok(build);
    }

    /// <summary>
    /// Gets the tests that were run on a build.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{id}/tests")]
    public async Task<ActionResult<IEnumerable<TestRun>>> GetTests(
        int id,
        CancellationToken cancellationToken)
    {
        var build = await buildService.GetAsync(
            id,
            cancellationToken);

        if (build is null)
            return NotFound();

        return Ok(build.TestRuns);
    }
}