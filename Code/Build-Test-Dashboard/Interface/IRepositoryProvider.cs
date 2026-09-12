using Build_Test_Dashboard.Requests;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Represents a provider for repository operations in the build and test dashboard.
/// </summary>
public interface IRepositoryProvider
{
    /// <summary>
    /// Gets the name of the provider.
    /// </summary>
    /// <value>
    /// The name of the provider.
    /// </value>
    string ProviderName { get; }

    /// <summary>
    /// Validates the connection asynchronous.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> ValidateConnectionAsync(
        CreateRepositoryRequest request,
        CancellationToken cancellationToken = default);
}