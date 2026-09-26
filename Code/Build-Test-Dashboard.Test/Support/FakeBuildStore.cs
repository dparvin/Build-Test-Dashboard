using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support
{
    /// <summary>
    /// A fake of the build store
    /// </summary>
    /// <seealso cref="IBuildStore" />
    public class FakeBuildStore : IBuildStore
    {
        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<Build>? Builds { get; set; }

        /// <summary>
        /// Stores the build asynchronous.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Build> StoreAsync(
            Build build,
            CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();

        /// <summary>
        /// Gets a build from a repository asynchronous.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Build?> GetAsync(
            int buildId,
            CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();

        /// <summary>
        /// Gets the by repository identifier asynchronous.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<IEnumerable<Build>> GetAllAsync(
            int repositoryId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Builds ??
                []);
        }

        /// <summary>
        /// Deletes the build asynchronous.
        /// </summary>
        /// <param name="repositoryId">The repository identifier.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> DeleteAsync(
            int buildId,
            CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
    }
}
