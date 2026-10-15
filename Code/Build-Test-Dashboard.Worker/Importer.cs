using Build_Test_Dashboard.Interface;

namespace Build_Test_Dashboard.Worker;

/// <summary>
/// Initializes a new instance of the <see cref="Importer"/> class.
/// </summary>
/// <param name="logger">The logger.</param>
internal class Importer(
    ILogger<Importer> logger) : IBuildImportService
{
    private readonly ILogger<Importer> logger = logger;

    public Task ImportBuildsAsync(CancellationToken cancellationToken)
    {
        // TODO: Discover configured build connections and import build data.
        logger.LogInformation("Starting build import cycle.");

        if (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Build import cycle canceled.");
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
