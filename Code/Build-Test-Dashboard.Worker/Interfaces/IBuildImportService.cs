namespace Build_Test_Dashboard.Worker.Interfaces;

/// <summary>
/// Imports build and test data from configured build connections.
/// </summary>
public interface IBuildImportService
{
    /// <summary>
    /// Imports builds asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    /// Token used to cancel the import operation.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task ImportBuildsAsync(CancellationToken cancellationToken);
}
