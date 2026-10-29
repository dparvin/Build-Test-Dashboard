using Build_Test_Dashboard.API.Interfaces;
using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.API.Stores
{
    /// <summary>
    /// Represents a credential store that uses Azure Key Vault for storing and retrieving credentials in the build and test dashboard.
    /// </summary>
    /// <seealso cref="ICredentialStore" />
    public class KeyVaultCredentialStore : ICredentialStore
    {
        /// <summary>
        /// Stores the credential asynchronously.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StoreAsync(Credential credential, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the credential asynchronously.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Credential?> GetAsync(int repositoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the credential asynchronously.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(int repositoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}