using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Build_Test_Dashboard.Test.Support;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public FakeHttpMessageHandler GitHubHandler { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<GitHubRepositoryProvider>();

            services.AddHttpClient<GitHubRepositoryProvider>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => GitHubHandler);

            services.AddScoped<IRepositoryProvider>(
                provider => provider.GetRequiredService<GitHubRepositoryProvider>());
        });
    }
}