using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Services;

/// <summary>
/// A service for managing build connections
/// </summary>
/// <param name="buildConnectionStore">The build connection store.</param>
/// <seealso cref="IBuildConnectionService" />
public class BuildConnectionService(
    IBuildConnectionStore buildConnectionStore) : IBuildConnectionService
{
    /// <summary>
    /// The build connection store.
    /// </summary>
    private readonly IBuildConnectionStore buildConnectionStore = buildConnectionStore;

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
        CancellationToken cancellationToken = default) =>
        buildConnectionStore.StoreAsync(
            buildConnection,
            cancellationToken);

    /// <summary>
    /// Gets all <see cref="BuildConnection" /> instances asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the <see cref="BuildConnection" /> instances.
    /// </returns>
    public Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        buildConnectionStore.GetAllAsync(
            cancellationToken);

    /// <summary>
    /// Gets a <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the <see cref="BuildConnection" /> is not found, or the
    /// <see cref="BuildConnection" /> if it is found.
    /// </returns>
    public Task<BuildConnection?> GetAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default) =>
        buildConnectionStore.GetAsync(
            buildConnectionId,
            cancellationToken);

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
        CancellationToken cancellationToken = default) =>
        buildConnectionStore.FindAsync(
            provider,
            name,
            cancellationToken);

    /// <summary>
    /// Deletes the <see cref="BuildConnection" /> asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the <see cref="BuildConnection" /> existed and was deleted;
    /// otherwise <c>false</c>.
    /// </returns>
    public Task<bool> DeleteAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default) =>
        buildConnectionStore.DeleteAsync(
            buildConnectionId,
            cancellationToken);
}