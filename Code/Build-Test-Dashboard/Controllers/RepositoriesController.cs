using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Controllers;

/// <summary>
/// Represents the controller for managing repositories in the build and test dashboard.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class RepositoriesController(
    RepositoryService repositoryService) : ControllerBase
{
    private readonly RepositoryService repositoryService = repositoryService;

    #region GET calls ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    /// <summary>
    /// Gets a list of repositories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Repository> Get()
    {
        return
        [
            new Repository
            {
                Id = 1,
                Name = "Build/Test Dashboard",
                Provider = "GitHub",
                Owner = "dparvin",
                RepositoryName = "Build-Test-Dashboard"
            },
            new Repository
            {
                Id = 2,
                Name = "PropertyGridHelpers",
                Provider = "GitHub",
                Owner = "dparvin",
                RepositoryName = "PropertyGridHelpers"
            }
        ];
    }

    /// <summary>
    /// Gets the specified repository.
    /// </summary>
    /// <param name="id">The identifier of the repository.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Repository? Get(int id)
    {
        return Get().FirstOrDefault(repository => repository.Id == id);
    }

    /// <summary>
    /// Gets the builds from a specific repository.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpGet("{id}/builds")]
    public IEnumerable<Build> GetBuilds(int id)
    {
        return
        [
            new Build
            {
                Id = 1,
                RepositoryId = id,
                BuildNumber = "100",
                Branch = "main",
                Commit = "abc123",
                Started = DateTime.UtcNow.AddMinutes(-10),
                Completed = DateTime.UtcNow.AddMinutes(-5),
                Status = "Succeeded"
            },
            new Build
            {
                Id = 2,
                RepositoryId = id,
                BuildNumber = "101",
                Branch = "main",
                Commit = "def456",
                Started = DateTime.UtcNow.AddMinutes(-4),
                Completed = DateTime.UtcNow,
                Status = "Succeeded"
            }
        ];
    }

    #endregion

    #region POST calls ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    /// <summary>
    /// Posts the specified repository.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Repository>> Post(
        Repository repository,
        CancellationToken cancellationToken)
    {
        var connectionValid =
            await repositoryService.ValidateConnectionAsync(
                repository,
                cancellationToken);

        if (!connectionValid)
            return Unauthorized();

        // We'll eventually save the repository here.

        return CreatedAtAction(
            nameof(Get),
            new { id = repository.Id },
            repository);
    }

    #endregion
}