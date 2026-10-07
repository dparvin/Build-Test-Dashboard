using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Represents a service for managing build connections.
/// </summary>
public interface IBuildConnectionService
{
    /// <summary>
    /// Saves the <see cref="BuildConnection"/> asynchronously.
    /// </summary>
    /// <param name="buildConnection">The build connection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="BuildConnection"/> that was saved.</returns>
    Task<BuildConnection> SaveAsync(
        BuildConnection buildConnection,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a <see cref="BuildConnection"/> asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the <see cref="BuildConnection"/> is not found, or the 
    /// <see cref="BuildConnection"/> if it is found.
    /// </returns>
    Task<BuildConnection?> GetAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all <see cref="BuildConnection"/> instances asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>All of the <see cref="BuildConnection"/> instances.</returns>
    Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the <see cref="BuildConnection"/> asynchronously.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the <see cref="BuildConnection"/> is not found, or the 
    /// <see cref="BuildConnection"/> if it is found.
    /// </returns>
    Task<BuildConnection?> FindAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the <see cref="BuildConnection"/> asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the <see cref="BuildConnection"/> existed and was deleted;
    /// otherwise <c>false</c>.
    /// </returns>
    Task<bool> DeleteAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default);
}
