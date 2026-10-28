using Build_Test_Dashboard.Worker.Interfaces;

namespace Build_Test_Dashboard.Worker.Test.Fakes;

/// <summary>
/// Represents a fake implementation of the <see cref="IBuildImportService"/> interface for testing purposes.
/// </summary>
/// <seealso cref="IBuildImportService" />
public class FakeBuildImportService : IBuildImportService
{
    /// <summary>
    /// The call count
    /// </summary>
    private int callCount;

    /// <summary>
    /// Gets the number of times the import service has been called.
    /// </summary>
    public int CallCount => Volatile.Read(ref callCount);

    /// <summary>
    /// Gets or sets the exception to throw on a specific call.
    /// </summary>
    public int? ExceptionOnCall { get; set; }

    /// <summary>
    /// Gets or sets the exception to throw on a specific call.
    /// </summary>
    public Exception? ExceptionToThrow { get; set; }

    /// <summary>
    /// Gets or sets an action invoked after each call is counted.
    /// </summary>
    public Action<int>? OnCall { get; set; }

    /// <summary>
    /// Gets a task that completes when the specified number of calls has occurred.
    /// </summary>
    public TaskCompletionSource<int> CallsReached { get; } =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    /// <summary>
    /// Gets or sets the number of calls required to complete <see cref="CallsReached"/>.
    /// </summary>
    public int ExpectedCallCount { get; set; } = 1;

    /// <summary>
    /// Imports builds asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the import operation.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public Task ImportBuildsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentCallCount = Interlocked.Increment(ref callCount);

        if (currentCallCount >= ExpectedCallCount)
            CallsReached.TrySetResult(currentCallCount);

        OnCall?.Invoke(currentCallCount);

        // Allow the OnCall callback to cancel the operation.
        cancellationToken.ThrowIfCancellationRequested();

        if (ExceptionOnCall == currentCallCount)
            return Task.FromException(
                ExceptionToThrow ??
                new InvalidOperationException("Simulated import failure."));

        return Task.CompletedTask;
    }
}