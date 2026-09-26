using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Controllers;

/// <summary>
/// Represents the controller for managing repositories in the build and test dashboard.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class RepositoriesController(
    IRepositoryService repositoryService,
    IBuildService buildService) : ControllerBase
{
    /// <summary>
    /// The repository service
    /// </summary>
    private readonly IRepositoryService repositoryService = repositoryService;
    /// <summary>
    /// The build service
    /// </summary>
    private readonly IBuildService buildService = buildService;

    #region GET calls ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    /// <summary>
    /// Gets a list of repositories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<Repository>> Get(
        CancellationToken cancellationToken)
    {
        return await repositoryService.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Gets the specified repository.
    /// </summary>
    /// <param name="id">The identifier of the repository.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Repository>> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var repository =
            await repositoryService.GetAsync(
                id,
                cancellationToken);

        if (repository is null)
            return NotFound();

        return Ok(repository);
    }

    /// <summary>
    /// Gets the builds from a specific repository.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpGet("{id}/builds")]
    public async Task<ActionResult<IEnumerable<Build>>> GetBuilds(
        int id,
        CancellationToken cancellationToken)
    {
        var builds =
            await buildService.GetAllAsync(
                id,
                cancellationToken);

        return Ok(builds);
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
        CreateRepositoryRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await repositoryService.SaveAsync(
                request,
                cancellationToken);

        if (result.ValidationResult != RepositoryValidationResult.Valid)
        {
            return result.ValidationResult switch
            {
                RepositoryValidationResult.InvalidCredentials =>
                    Unauthorized(),

                RepositoryValidationResult.OwnerNotFound =>
                    NotFound("Repository owner was not found."),

                RepositoryValidationResult.ProjectNotFound =>
                    NotFound("Repository project was not found."),

                RepositoryValidationResult.RepositoryNotFound =>
                    NotFound("Repository was not found."),

                RepositoryValidationResult.UnknownError =>
                    StatusCode(StatusCodes.Status502BadGateway),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Ok(result.Repository);
    }

    #endregion
}