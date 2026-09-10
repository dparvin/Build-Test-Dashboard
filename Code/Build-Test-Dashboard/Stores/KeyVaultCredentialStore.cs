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
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task DeleteAsync(int repositoryId) => throw new NotImplementedException();
        /// <summary>
        /// Gets the credential asynchronously.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<RepositoryCredential?> GetAsync(int repositoryId) => throw new NotImplementedException();
        /// <summary>
        /// Stores the credential asynchronously.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task StoreAsync(RepositoryCredential credential) => throw new NotImplementedException();
    }
}
