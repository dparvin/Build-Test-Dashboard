using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// Provides database storage for build connections.
/// </summary>
public class BuildConnectionStore(DashboardDbContext context)
    : IBuildConnectionStore
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly DashboardDbContext context = context;

    /// <summary>
    /// Stores the build connection asynchronously.
    /// </summary>
    /// <param name="buildConnection">The build connection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The stored build connection.
    /// </returns>
    public async Task<BuildConnection> StoreAsync(
        BuildConnection buildConnection,
        CancellationToken cancellationToken = default)
    {
        context.BuildConnections.Add(buildConnection);
        await context.SaveChangesAsync(cancellationToken);
        return buildConnection;
    }

    /// <summary>
    /// Gets a build connection asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>null</c> if the build connection is not found, or the build
    /// connection if it is found.
    /// </returns>
    public async Task<BuildConnection?> GetAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default) =>
        await context.BuildConnections
            .FindAsync(
                [buildConnectionId],
                cancellationToken);

    /// <summary>
    /// Gets all build connections asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// All of the build connections.
    /// </returns>
    public async Task<IEnumerable<BuildConnection>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await context.BuildConnections
            .OrderBy(connection => connection.Id)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Finds the BuildConnection asynchronously.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<BuildConnection?> FindAsync(
       string provider,
       string name,
       CancellationToken cancellationToken = default) =>
       await context.BuildConnections
           .FirstOrDefaultAsync(
               connection =>
                   connection.Provider == provider &&
                   connection.Name == name,
               cancellationToken);

    /// <summary>
    /// Deletes the build connection asynchronously.
    /// </summary>
    /// <param name="buildConnectionId">The build connection identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <c>true</c> when the build connection existed and was deleted;
    /// otherwise <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(
        int buildConnectionId,
        CancellationToken cancellationToken = default)
    {
        var buildConnection = await GetAsync(
            buildConnectionId,
            cancellationToken);

        if (buildConnection is null)
            return false;

        context.BuildConnections.Remove(buildConnection);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}