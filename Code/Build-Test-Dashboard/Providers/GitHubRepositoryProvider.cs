using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Requests;
using System.Net.Http.Headers;

namespace Build_Test_Dashboard.Providers;

/// <summary>
/// Represents a provider for GitHub repository operations in the build and test dashboard.
/// </summary>
public class GitHubRepositoryProvider(
    HttpClient httpClient) : IRepositoryProvider
{
    /// <summary>
    /// Gets the name of the provider.
    /// </summary>
    /// <value>
    /// The name of the provider.
    /// </value>
    public string ProviderName => "GitHub";

    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Validates the connection asynchronous.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> ValidateConnectionAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var repository = request.Repository;
        var credential = request.Credential;

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"repos/{repository.Owner}/{repository.RepositoryName}");

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", credential.Secret);

        httpRequest.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        using var response =
            await _httpClient.SendAsync(httpRequest, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}