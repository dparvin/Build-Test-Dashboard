using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Interfaces;

namespace Build_Test_Dashboard.Test.Support;

public class FakeBuildConnectionStore : IBuildConnectionStore
{
    /// <summary>
    /// Gets the stored BuildConnection.
    /// </summary>
    /// <value>
    /// The stored BuildConnection.
    /// </value>
    public BuildConnection? StoredBuildConnection { get; private set; }
    /// <summary>
    /// Gets or sets the build connections.
    /// </summary>
    /// <value>
    /// The build connections.
    /// </value>
    public IEnumerable<BuildConnection>? BuildConnections { get; set; }
    /// <summary>
    /// Gets or sets the find result.
    /// </summary>
    /// <value>
    /// The find result.
    /// </value>
    public BuildConnection? FindResult { get; set; }

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
    /// Gets the get all call count.
    /// </summary>
    /// <value>
    /// The get all call count.
    /// </value>
    public int GetAllCallCount { get; private set; }

    /// <summary>
    /// Stores the build connection asynchronously.
    /// </summary>
    /// <param name="buildConnection">The build connection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The stored build connection.
    /// </returns>
    public Task<BuildConnection> StoreAsync(
        BuildConnection buildConnection,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;

        if (StoreException != null)
            throw StoreException;

        if (buildConnection.Id == 0)
            buildConnection.Id = 1;

        StoredBuildConnection = buildConnection;

        return Task.FromResult(buildConnection);
    }

    /// <summary>
    /// Gets all build connections asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the build connections.
    /// </returns>
    public Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        GetAllCallCount++;
        return Task.FromResult(
            BuildConnections ??
            []);
    }

    /// <summary>
    /// Gets the build connection asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<BuildConnection?> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (StoredBuildConnection is not null)
        {
            return Task.FromResult(
                StoredBuildConnection.Id == id
                    ? StoredBuildConnection
                    : null);
        }

        if (BuildConnections is not null)
        {
            return Task.FromResult(
                BuildConnections.FirstOrDefault(
                    repository => repository.Id == id));
        }

        return Task.FromResult<BuildConnection?>(null);
    }

    /// <summary>
    /// Finds the BuildConnection asynchronously.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<BuildConnection?> FindAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default)
    {
        FindCallCount++;

        return Task.FromResult(FindResult);
    }

    /// <summary>
    /// Deletes the build connection asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        DeleteCallCount++;

        if (DeleteException != null)
            throw DeleteException;

        if (StoredBuildConnection is not null)
        {
            if (StoredBuildConnection.Id == id)
            {
                StoredBuildConnection = null;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        if (BuildConnections is not null)
        {
            var buildToDelete = BuildConnections.FirstOrDefault(
                repository => repository.Id == id);
            if (buildToDelete is not null)
            {
                BuildConnections = BuildConnections.Where(
                    repository => repository.Id != id);
                return Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }
}