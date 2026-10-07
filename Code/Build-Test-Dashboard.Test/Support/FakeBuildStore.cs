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
        /// Gets the stored Build.
        /// </summary>
        /// <value>
        /// The stored Build.
        /// </value>
        public Build? StoredBuild { get; private set; }
        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<Build>? Builds { get; set; }
        /// <summary>
        /// Gets or sets the find result.
        /// </summary>
        /// <value>
        /// The find result.
        /// </value>
        public Build? FindResult { get; set; }

        /// <summary>
        /// Gets or sets the store exception.
        /// </summary>
        /// <value>
        /// The store exception.
        /// </value>
        public Exception? StoreException { get; set; } = null;

        /// <summary>
        /// Gets or sets the delete exception.
        /// </summary>
        /// <value>
        /// The delete exception.
        /// </value>
        public Exception? DeleteException { get; set; } = null;

        /// <summary>
        /// Gets the find call count.
        /// </summary>
        /// <value>
        /// The find call count.
        /// </value>
        public int FindCallCount { get; private set; }
        /// <summary>
        /// Gets the store call count.
        /// </summary>
        /// <value>
        /// The store call count.
        /// </value>
        public int StoreCallCount { get; private set; }
        /// <summary>
        /// Gets the delete call count.
        /// </summary>
        /// <value>
        /// The delete call count.
        /// </value>
        public int DeleteCallCount { get; private set; }

        /// <summary>
        /// Stores the build asynchronous.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Build> StoreAsync(
            Build build,
            CancellationToken cancellationToken = default)
        {
            StoreCallCount++;

            if (StoreException != null)
                throw StoreException;

            if (build.Id == 0)
                build.Id = 1;

            StoredBuild = build;

            return Task.FromResult(build);
        }

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
            CancellationToken cancellationToken = default)
        {
            if (StoredBuild is not null)
            {
                return Task.FromResult(
                    StoredBuild.Id == buildId
                        ? StoredBuild
                        : null);
            }

            if (Builds is not null)
            {
                return Task.FromResult(
                    Builds.FirstOrDefault(
                        repository => repository.Id == buildId));
            }

            return Task.FromResult<Build?>(null);
        }

        /// <summary>
        /// Gets the by repository identifier asynchronous.
        /// </summary>
        /// <param name="buildConnectionId">The build connection identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// All of the builds for a repository.
        /// </returns>
        public Task<IEnumerable<Build>> GetAllAsync(
            int buildConnectionId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Builds ??
                []);
        }

        /// <summary>
        /// Gets the by source repository asynchronous.
        /// </summary>
        /// <param name="sourceRepositoryId">The source repository identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<IEnumerable<Build>> GetBySourceRepositoryAsync(
            int sourceRepositoryId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Builds ??
                []);
        }

        /// <summary>
        /// Finds the build asynchronously.
        /// </summary>
        /// <param name="buildConnectionId">The build connection identifier.</param>
        /// <param name="externalBuildId">The external build identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <c>null</c> if the build is not found, or the build record if it is found.
        /// </returns>
        public Task<Build?> FindAsync(
            int buildConnectionId,
            string externalBuildId,
            CancellationToken cancellationToken = default)
        {
            FindCallCount++;

            return Task.FromResult(FindResult);
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
            CancellationToken cancellationToken = default)
        {
            DeleteCallCount++;

            if (DeleteException != null)
                throw DeleteException;

            if (StoredBuild?.Id != buildId)
                return Task.FromResult(false);

            StoredBuild = null;

            return Task.FromResult(true);
        }
    }
}
