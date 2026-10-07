using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Interface for the BuildConnection Store.
/// </summary>
public interface IBuildConnectionStore
{
    /// <summary>
    /// Stores the build connection asynchronously.
    /// </summary>
    /// <param name="buildConnection">The build connection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The stored build connection.</returns>
    Task<BuildConnection> StoreAsync(
        BuildConnection buildConnection,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a build connection asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the build connection is not found, or the build
    /// connection if it is found.
    /// </returns>
    Task<BuildConnection?> GetAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all build connections asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>All of the build connections.</returns>
    Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the BuildConnection asynchronously.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<BuildConnection?> FindAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the build connection asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the build connection existed and was deleted;
    /// otherwise <c>false</c>.
    /// </returns>
    Task<bool> DeleteAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default);
}