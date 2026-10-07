using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Services;

namespace Build_Test_Dashboard.Test.Support;

public class FakeBuildService(
    BuildService realBuildService,
    FakeBuildServiceState state) : IBuildService
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    public FakeBuildServiceState State { get; } = state;

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
    /// Gets the stored build.
    /// </summary>
    /// <value>
    /// The stored build.
    /// </value>
    public Build? StoredBuild { get; set; } = null;

    /// <summary>
    /// Stores the build asynchronously.
    /// </summary>
    /// <param name="build">The build.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The stored build.
    /// </returns>
    public Task<Build> SaveAsync(
        Build build,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;

        if (State.UseRealService)
            return realBuildService.SaveAsync(build, cancellationToken);

        if (StoreException != null)
            throw StoreException;

        if (State.BuildResult == null)
            if (StoredBuild != null)
                return Task.FromResult(StoredBuild);
            else
                return Task.FromResult<Build>(new Build());
        return Task.FromResult(State.BuildResult);
    }

    /// <summary>
    /// Deletes the build asynchronously.
    /// </summary>
    /// <param name="buildId">The build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the build existed and was deleted; otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteAsync(
        int buildId,
        CancellationToken cancellationToken = default)
    {
        DeleteCallCount++;

        if (State.UseRealService)
            await realBuildService.DeleteAsync(buildId, cancellationToken);

        if (DeleteException != null)
            throw DeleteException;

        if (StoredBuild?.Id != buildId)
            return await Task.FromResult(false);

        StoredBuild = null;

        return await Task.FromResult(true);
    }

    /// <summary>
    /// Finds the build asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="externalBuildId">The external build identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Build?> FindAsync(
        int repositoryId,
        string externalBuildId,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildService.FindAsync(repositoryId, externalBuildId, cancellationToken);

        return Task.FromResult(StoredBuild);
    }

    /// <summary>
    /// Gets all builds for a repository asynchronously.
    /// </summary>
    /// <param name="repositoryId">The repository identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the builds for a repository.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Build>> GetAllAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildService.GetAllAsync(
                repositoryId,
                cancellationToken);

        IEnumerable<Build> list;

        list = State.BuildList ?? [];

        return Task.FromResult(list);
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
        if (State.UseRealService)
            return realBuildService.GetBySourceRepositoryAsync(
                sourceRepositoryId,
                cancellationToken);

        IEnumerable<Build> list;

        list = State.BuildList ?? [];

        return Task.FromResult(list);
    }

    /// <summary>
    /// Gets a build asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the build is not found, or the build record if it is found.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Build?> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildService.GetAsync(
                id,
                cancellationToken);

        IEnumerable<Build> list;

        list = State.BuildList ?? [];

        return Task.FromResult(list.FirstOrDefault(repository => repository.Id == id));
    }
}
