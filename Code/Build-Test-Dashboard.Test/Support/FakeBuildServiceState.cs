using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// State for use in testing BuildService
/// </summary>
public class FakeBuildServiceState
{
    /// <summary>
    /// Gets or sets a value indicating whether use real service.
    /// </summary>
    /// <value>
    ///   <c>true</c> if use real service; otherwise, <c>false</c>.
    /// </value>
    public bool UseRealService { get; set; } = true;

    /// <summary>
    /// Gets or sets the build.
    /// </summary>
    /// <value>
    /// The build.
    /// </value>
    public Build? BuildResult { get; set; } = null;

    /// <summary>
    /// Gets or sets the build list.
    /// </summary>
    /// <value>
    /// The build list.
    /// </value>
    public IEnumerable<Build>? BuildList { get; set; } = null;

}
