using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Requests;
using System.Net;
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
    public async Task<RepositoryValidationResult> ValidateRepositoryAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var ownerResult = await ValidateOwnerAsync(request, cancellationToken);
        if (ownerResult != RepositoryValidationResult.Valid)
            return ownerResult;
        var repository = request.Repository;
        var credential = request.Credential;

        using var repositoryRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"repos/{repository.Owner}/{repository.RepositoryName}");

        repositoryRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", credential.Secret);

        repositoryRequest.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        using var repositoryResponse =
            await _httpClient.SendAsync(repositoryRequest, cancellationToken);

        return repositoryResponse.StatusCode == HttpStatusCode.OK ?
                RepositoryValidationResult.Valid :
                repositoryResponse.StatusCode == HttpStatusCode.Unauthorized ?
                    RepositoryValidationResult.InvalidCredentials :
                    repositoryResponse.StatusCode == HttpStatusCode.NotFound ?
                        RepositoryValidationResult.RepositoryNotFound :
                        RepositoryValidationResult.UnknownError;
    }

    /// <summary>
    /// Validates the owner asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    private async Task<RepositoryValidationResult> ValidateOwnerAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var repository = request.Repository;
        //var credential = request.Credential;

        using var ownerRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"users/{repository.Owner}");

        //ownerRequest.Headers.Authorization =
        //    new AuthenticationHeaderValue("Bearer", credential.Secret);

        ownerRequest.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        try
        {
            using var ownerResponse =
                await _httpClient.SendAsync(
                    ownerRequest,
                    cancellationToken);

            return ownerResponse.StatusCode == HttpStatusCode.OK ?
                    RepositoryValidationResult.Valid :
                    ownerResponse.StatusCode == HttpStatusCode.Unauthorized ?
                        RepositoryValidationResult.InvalidCredentials :
                        ownerResponse.StatusCode == HttpStatusCode.NotFound ?
                            RepositoryValidationResult.OwnerNotFound :
                            RepositoryValidationResult.UnknownError;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return RepositoryValidationResult.UnknownError;
        }
    }
}