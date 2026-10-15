using Build_Test_Dashboard.Interface;

namespace Build_Test_Dashboard.Worker;

/// <summary>
/// Provides the entry point for the build and test data import worker.
/// </summary>
public class Program
{
    internal static CancellationToken CancellationToken { get; set; }

    /// <summary>
    /// Configures and runs the worker host.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Build Test Dashboard Worker";
        });

        // Add services to the container.
        builder.Services.AddDashboardServices(builder.Configuration);
        builder.Services.AddScoped<IBuildImportService, Importer>();

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        await host.RunAsync(CancellationToken).ConfigureAwait(false);
    }
}