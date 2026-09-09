namespace Build_Test_Dashboard.Models
{
    /// <summary>
    /// Represents a test run in the build and test dashboard.
    /// </summary>
    public class TestRun
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the build identifier.
        /// </summary>
        /// <value>
        /// The build identifier.
        /// </value>
        public int BuildId { get; set; }

        /// <summary>
        /// Gets or sets the total number of tests.
        /// </summary>
        /// <value>
        /// The total number of tests.
        /// </value>
        public int Total { get; set; }
        /// <summary>
        /// Gets or sets the passed.
        /// </summary>
        /// <value>
        /// The passed.
        /// </value>
        public int Passed { get; set; }
        /// <summary>
        /// Gets or sets the failed.
        /// </summary>
        /// <value>
        /// The failed.
        /// </value>
        public int Failed { get; set; }
        /// <summary>
        /// Gets or sets the skipped.
        /// </summary>
        /// <value>
        /// The skipped.
        /// </value>
        public int Skipped { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; set; }
    }
}
