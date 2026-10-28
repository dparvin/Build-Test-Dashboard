using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Interfaces;

namespace Build_Test_Dashboard.Stores
{
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