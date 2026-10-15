using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Worker.Test.Fakes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Build_Test_Dashboard.Worker.Test;

/// <summary>
/// Represents unit tests for the Worker class in the Build-Test-Dashboard.Worker project.
/// </summary>
public class WorkerTests
{
    /// <summary>
    /// Tests when Execute calls the import service asynchronously.
    /// </summary>
    [Fact]
    public async Task ExecuteAsync_CallsImportService()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService
        {
            OnCall = _ => cancellation.Cancel()
        };

        var worker = CreateWorker(importService, 1);

        await worker.ExecuteWorkerAsync(cancellation.Token);

        Assert.Equal(1, importService.CallCount);
    }

    /// <summary>
    /// Tests when Execute continues after import failure asynchronously.
    /// </summary>
    [Fact]
    public async Task ExecuteAsync_ContinuesAfterImportFailure()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService
        {
            ExceptionOnCall = 1,
            ExpectedCallCount = 2,
            OnCall = callCount =>
            {
                if (callCount == 2)
                {
                    cancellation.Cancel();
                }
            }
        };

        var worker = CreateWorker(importService, 1);

        await worker.ExecuteWorkerAsync(cancellation.Token);

        Assert.Equal(2, importService.CallCount);
    }

    /// <summary>
    /// Tests when Execute stops when cancellation is requested asynchronously.
    /// </summary>
    [Fact]
    public async Task ExecuteAsync_StopsWhenCancellationRequested()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService
        {
            OnCall = _ => cancellation.Cancel()
        };

        var worker = CreateWorker(importService, 1);

        await worker.ExecuteWorkerAsync(cancellation.Token);

        Assert.Equal(1, importService.CallCount);
    }

    /// <summary>
    /// Executes the asynchronous invalid polling interval throws.
    /// </summary>
    /// <param name="pollingInterval">The polling interval.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ExecuteAsync_InvalidPollingInterval_Throws(
        int pollingInterval)
    {
        var importService = new FakeBuildImportService();
        var worker = CreateWorker(importService, pollingInterval);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => worker.ExecuteWorkerAsync(CancellationToken.None));

        Assert.Equal(0, importService.CallCount);
    }

    /// <summary>
    /// Executes the asynchronous logs startup information.
    /// </summary>
    [Fact]
    public async Task ExecuteAsync_LogsStartupInformation()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService
        {
            OnCall = _ => cancellation.Cancel()
        };

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Information)
                .AddConsole();

        });

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ImportSettings:PollingIntervalSeconds"] = "1"
                })
            .Build();

        var services = new ServiceCollection();
        services.AddScoped<IBuildImportService>(_ => importService);

        using var serviceProvider = services.BuildServiceProvider();

        var worker = new Worker(
            loggerFactory.CreateLogger<Worker>(),
            configuration,
            serviceProvider.GetRequiredService<IServiceScopeFactory>());

        await worker.ExecuteWorkerAsync(cancellation.Token);

        Assert.Equal(1, importService.CallCount);
    }

    /// <summary>
    /// Executes the asynchronous stops when import is canceled.
    /// </summary>
    [Fact]
    public async Task ExecuteAsync_StopsWhenImportIsCanceled()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService
        {
            OnCall = _ => cancellation.Cancel()
        };

        var worker = CreateWorker(importService, 1);

        await worker.ExecuteWorkerAsync(cancellation.Token);

        Assert.Equal(1, importService.CallCount);
    }

    [Fact]
    public async Task ExecuteAsync_StopsWhenDelayIsCanceled()
    {
        using var cancellation = new CancellationTokenSource();

        var importService = new FakeBuildImportService();
        var worker = CreateWorker(importService, 60);

        var workerTask = worker.ExecuteWorkerAsync(cancellation.Token);

        await importService.CallsReached.Task;

        // Allow the import to finish and the worker to enter its delay.
        await Task.Delay(1000, cancellation.Token);
        cancellation.Cancel();

        await workerTask;

        Assert.Equal(1, importService.CallCount);
    }

    /// <summary>
    /// Creates the worker.
    /// </summary>
    /// <param name="importService">The import service.</param>
    /// <returns></returns>
    private static Worker CreateWorker(
        FakeBuildImportService importService,
        int pollingInterval)
    {
        var services = new ServiceCollection();

        services.AddLogging();

        services.AddScoped<IBuildImportService>(
            _ => importService);

        var serviceProvider = services.BuildServiceProvider();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ImportSettings:PollingIntervalSeconds"] = $"{pollingInterval}"
                })
            .Build();

        return new Worker(
            NullLogger<Worker>.Instance,
            configuration,
            serviceProvider.GetRequiredService<IServiceScopeFactory>());
    }
}
