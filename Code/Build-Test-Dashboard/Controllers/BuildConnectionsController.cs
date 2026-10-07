using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Build_Test_Dashboard.Controllers
{
    /// <summary>
    /// Represents the controller for managing build connections in the build and test dashboard.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BuildConnectionsController(
        IBuildConnectionService buildConnectionService) : ControllerBase
    {
        /// <summary>
        /// The build connection service
        /// </summary>
        private readonly IBuildConnectionService buildConnectionService =
            buildConnectionService;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildConnection>>> GetAll(
            CancellationToken cancellationToken)
        {
            var buildConnections =
                await buildConnectionService.GetAllAsync(cancellationToken);

            return Ok(buildConnections);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildConnection>> Get(
            int id,
            CancellationToken cancellationToken)
        {
            var buildConnection =
                await buildConnectionService.GetAsync(
                    id,
                    cancellationToken);

            if (buildConnection is null)
                return NotFound();

            return Ok(buildConnection);
        }

        [HttpPost]
        public async Task<ActionResult<BuildConnection>> Post(
            BuildConnection buildConnection,
            CancellationToken cancellationToken)
        {
            var result =
                await buildConnectionService.SaveAsync(
                    buildConnection,
                    cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var deleted =
                await buildConnectionService.DeleteAsync(
                    id,
                    cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
