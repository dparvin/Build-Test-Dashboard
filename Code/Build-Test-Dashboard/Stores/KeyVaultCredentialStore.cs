using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Stores
{
    public class KeyVaultCredentialStore : ICredentialStore
    {
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

        /// <summary>
        /// Gets the credential asynchronously.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<RepositoryCredential?> GetAsync(int repositoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stores the credential asynchronously.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StoreAsync(RepositoryCredential credential, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}