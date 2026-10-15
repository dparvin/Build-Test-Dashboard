using Build_Test_Dashboard.Interface;

namespace Build_Test_Dashboard.Worker;

public class Worker(
    ILogger<Worker> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var pollingInterval = configuration.GetValue(
            "ImportSettings:PollingIntervalSeconds", 60);

        if (pollingInterval <= 0)
            throw new InvalidOperationException(
                "ImportSettings:PollingIntervalSeconds must be greater than zero.");

        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation(
                "Build import worker started. Polling interval: {PollingIntervalSeconds} seconds.",
                pollingInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var importService =
                    scope.ServiceProvider.GetRequiredService<IBuildImportService>();

                await importService.ImportBuildsAsync(stoppingToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (
                stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception,
                    "An error occurred during the build import cycle.");
            }

            try
            {
                if (!stoppingToken.IsCancellationRequested)
                    await Task.Delay(
                        TimeSpan.FromSeconds(pollingInterval),
                        stoppingToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (
                stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }

        logger.LogInformation("Build import worker stopped.");
    }

    /// <summary>
    /// Executes the worker asynchronously.
    /// </summary>
    /// <param name="stoppingToken">The stopping token.</param>
    internal async Task ExecuteWorkerAsync(
        CancellationToken stoppingToken)
    {
        await ExecuteAsync(stoppingToken).ConfigureAwait(false);
    }
}