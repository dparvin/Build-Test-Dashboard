using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface
{
    /// <summary>
    /// Represents a provider for repository operations in the build and test dashboard.
    /// </summary>
    public interface IRepositoryProvider
    {
        string ProviderName { get; }

        /// <summary>
        /// Validates the connection asynchronous.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> ValidateConnectionAsync(
            Repository repository,
            CancellationToken cancellationToken = default);
    }
}
