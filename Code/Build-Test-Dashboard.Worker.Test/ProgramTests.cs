namespace Build_Test_Dashboard.Worker.Test
{
    /// <summary>
    /// Represents unit tests for the Program class in the Build-Test-Dashboard.Worker project.
    /// </summary>
    public class ProgramTests
    {
        [Fact]
        public async Task RunAsync_StopsWhenCanceled()
        {
            using var cancellation = new CancellationTokenSource();
            Program.CancellationToken = cancellation.Token;

            var hostTask = Program.Main([]);

            await cancellation.CancelAsync();

            await hostTask;
        }
    }
}