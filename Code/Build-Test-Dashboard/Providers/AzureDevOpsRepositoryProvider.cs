// Ignore Spelling: Dev

using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Providers
{
    /// <summary>
    /// Represents a provider for Azure DevOps repository operations in the build and test dashboard.
    /// </summary>
    public class AzureDevOpsRepositoryProvider : IRepositoryProvider
    {
        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        public string ProviderName => "AzureDevOps";

        /// <summary>
        /// Validates the connection asynchronous.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> ValidateConnectionAsync(Repository repository, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
