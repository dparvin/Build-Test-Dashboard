using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Services
{
    /// <summary>
    /// Represents a service for managing repositories in the build and test dashboard.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RepositoryService"/> class.
    /// </remarks>
    /// <param name="repositoryProvider">The repository provider.</param>
    public class RepositoryService(
        IEnumerable<IRepositoryProvider> providers)
    {
        /// <summary>
        /// The repository provider
        /// </summary>
        private readonly IEnumerable<IRepositoryProvider> repositoryProvider = providers;

        /// <summary>
        /// Validates the connection asynchronous.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> ValidateConnectionAsync(
            Repository repository,
            CancellationToken cancellationToken = default)
        {
            foreach (var provider in repositoryProvider)
            {
                if (string.Compare(repository.Provider, provider.ProviderName, StringComparison.OrdinalIgnoreCase) == 0)
                    return await provider.ValidateConnectionAsync(repository, cancellationToken);
            }

            throw new NotSupportedException(
                $"The repository provider '{repository.Provider}' is not supported.");
        }
    }
}