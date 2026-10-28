using Build_Test_Dashboard.Interfaces;
using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Services;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// A fake of the build connection service
/// </summary>
/// <param name="realBuildConnectionService">The real build connection service.</param>
/// <param name="state">The state.</param>
/// <seealso cref="IBuildConnectionService" />
public class FakeBuildConnectionService(
    BuildConnectionService realBuildConnectionService,
    FakeBuildConnectionServiceState state) : IBuildConnectionService
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    public FakeBuildConnectionServiceState State { get; } = state;
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
    /// Gets the stored repository.
    /// </summary>
    /// <value>
    /// The stored repository.
    /// </value>
    public BuildConnection? StoredBuildConnection { get; set; } = null;

    /// <summary>
    /// Saves the <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="buildConnection">The build connection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The <see cref="BuildConnection" /> that was saved.
    /// </returns>
    public Task<BuildConnection> SaveAsync(
        BuildConnection buildConnection,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;

        if (State.UseRealService)
            return realBuildConnectionService.SaveAsync(buildConnection, cancellationToken);

        if (StoreException != null)
            throw StoreException;

        if (State.BuildConnectionResult is not null)
            return Task.FromResult(State.BuildConnectionResult);

        if (buildConnection.Id == 0)
        {
            buildConnection.Id = State.BuildConnectionList is not null
                ? State.BuildConnectionList.Select(connection => connection.Id).DefaultIfEmpty(0).Max() + 1
                : 1;
        }

        if (State.BuildConnectionList is not null)
            State.BuildConnectionList.Add(buildConnection);
        else
            State.BuildConnectionList = [buildConnection];

        StoredBuildConnection = buildConnection;
        return Task.FromResult(StoredBuildConnection);
    }

    /// <summary>
    /// Gets all <see cref="BuildConnection" /> instances asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the <see cref="BuildConnection" /> instances.
    /// </returns>
    public Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildConnectionService.GetAllAsync(
                cancellationToken);

        IEnumerable<BuildConnection> list =
            State.BuildConnectionList ?? [];

        return Task.FromResult(list);
    }

    /// <summary>
    /// Gets a <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="id">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the <see cref="BuildConnection" /> is not found, or the
    /// <see cref="BuildConnection" /> if it is found.
    /// </returns>
    public Task<BuildConnection?> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildConnectionService.GetAsync(
                id,
                cancellationToken);

        IEnumerable<BuildConnection> list;

        list = State.BuildConnectionList ?? [];

        return Task.FromResult(list.FirstOrDefault(buildConnection => buildConnection.Id == id));
    }

    /// <summary>
    /// Finds the <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the <see cref="BuildConnection" /> is not found, or the
    /// <see cref="BuildConnection" /> if it is found.
    /// </returns>
    public Task<BuildConnection?> FindAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default)
    {
        if (State.UseRealService)
            return realBuildConnectionService.FindAsync(provider, name, cancellationToken);

        return Task.FromResult(StoredBuildConnection);
    }

    /// <summary>
    /// Deletes the <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="id">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the <see cref="BuildConnection" /> existed and was deleted;
    /// otherwise <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        DeleteCallCount++;

        if (State.UseRealService)
            return await realBuildConnectionService.DeleteAsync(id, cancellationToken);

        if (DeleteException != null)
            throw DeleteException;

        if (StoredBuildConnection is not null)
        {
            if (StoredBuildConnection.Id != id)
                return await Task.FromResult(false);

            StoredBuildConnection = null;
            return await Task.FromResult(true);
        }

        if (State.BuildConnectionList is not null)
        {
            var buildConnection = State.BuildConnectionList
                .FirstOrDefault(connection => connection.Id == id);

            if (buildConnection is null)
                return await Task.FromResult(false);

            State.BuildConnectionList.Remove(buildConnection);
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }
}