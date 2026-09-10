using Build_Test_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Controllers;

/// <summary>
/// Represents the controller for managing builds in the build and test dashboard.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class BuildsController : ControllerBase
{
    [HttpGet("{id}")]
    public Build? Get(int id)
    {
        // Temporary test data
        return new Build
        {
            Id = id,
            RepositoryId = 1,
            BuildNumber = "100",
            Branch = "main",
            Commit = "abc123",
            Started = DateTime.UtcNow.AddMinutes(-10),
            Completed = DateTime.UtcNow.AddMinutes(-5),
            Status = "Succeeded"
        };
    }

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