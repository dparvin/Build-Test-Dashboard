// Ignore Spelling: Dev

using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Providers;
using Build_Test_Dashboard.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// Represents a test web application factory for creating a test server and client for integration testing.
/// </summary>
/// <seealso cref="WebApplicationFactory&lt;Program&gt;" />
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <summary>
    /// Gets the git hub handler.
    /// </summary>
    /// <value>
    /// The git hub handler.
    /// </value>
    public FakeHttpMessageHandler GitHubHandler { get; } = new();

    /// <summary>
    /// Gets the azure dev ops handler.
    /// </summary>
    /// <value>
    /// The azure dev ops handler.
    /// </value>
    public FakeHttpMessageHandler AzureDevOpsHandler { get; } = new();

    /// <summary>
    /// Gets the repository store.
    /// </summary>
    /// <value>
    /// The repository store.
    /// </value>
    public FakeRepositoryStore RepositoryStore { get; } = new();

    /// <summary>
    /// Gets the credential store.
    /// </summary>
    /// <value>
    /// The credential store.
    /// </value>
    public FakeCredentialStore CredentialStore { get; } = new();

    /// <summary>
    /// Gets the repository service.
    /// </summary>
    /// <value>
    /// The repository service.
    /// </value>
    public FakeRepositoryServiceState RepositoryServiceState { get; } = new();

    /// <summary>
    /// Gives a fixture an opportunity to configure the application before it gets built.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> for the application.</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IRepositoryProvider>();
            services.RemoveAll<GitHubRepositoryProvider>();
            services.RemoveAll<AzureDevOpsRepositoryProvider>();
            services.RemoveAll<IRepositoryStore>();
            services.RemoveAll<ICredentialStore>();
            services.RemoveAll<IRepositoryService>();

            services.AddSingleton(RepositoryServiceState);

            services.AddScoped<RepositoryService>();
            services.AddScoped<FakeRepositoryService>();

            services.AddScoped<IRepositoryStore>(
                _ => RepositoryStore);
            services.AddScoped<ICredentialStore>(
                _ => CredentialStore);
            services.AddScoped<IRepositoryService>(
                provider => provider.GetRequiredService<FakeRepositoryService>());

            services.AddHttpClient<GitHubRepositoryProvider>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => GitHubHandler);

            services.AddHttpClient<AzureDevOpsRepositoryProvider>(client =>
            {
                client.BaseAddress = new Uri("https://dev.azure.com/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => AzureDevOpsHandler);

            services.AddScoped<IRepositoryProvider>(
                provider => provider.GetRequiredService<GitHubRepositoryProvider>());
            services.AddScoped<IRepositoryProvider>(
                provider => provider.GetRequiredService<AzureDevOpsRepositoryProvider>());
        });
    }
}