// Ignore Spelling: Dev

using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Build_Test_Dashboard.Providers;

/// <summary>
/// Represents a provider for Azure DevOps repository operations in the build and test dashboard.
/// </summary>
public class AzureDevOpsRepositoryProvider(
    HttpClient httpClient) : IRepositoryProvider
{
    /// <summary>
    /// Gets the name of the provider.
    /// </summary>
    /// <value>
    /// The name of the provider.
    /// </value>
    public string ProviderName => "AzureDevOps";

    /// <summary>
    /// The HTTP client.
    /// </summary>
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Validates the connection asynchronous.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var repository = request.Repository;
        var credential = request.Credential;

        var authentication = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($":{credential.Secret}"));

        using var repositoryRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"{repository.Owner}/{repository.Project}/_apis/git/repositories/{repository.RepositoryName}?api-version=7.1");

        repositoryRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Basic", authentication);

        repositoryRequest.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        using var repositoryResponse =
            await _httpClient.SendAsync(
                repositoryRequest,
                cancellationToken);

        return repositoryResponse.StatusCode == HttpStatusCode.OK ?
                RepositoryValidationResult.Valid :
                repositoryResponse.StatusCode == HttpStatusCode.Unauthorized ?
                    RepositoryValidationResult.InvalidCredentials :
                    repositoryResponse.StatusCode == HttpStatusCode.NotFound ?
                        RepositoryValidationResult.RepositoryNotFound :
                        RepositoryValidationResult.UnknownError;
    }
}