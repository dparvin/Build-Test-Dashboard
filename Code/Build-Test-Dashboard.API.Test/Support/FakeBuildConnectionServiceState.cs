using Build_Test_Dashboard.Contracts.Models;

namespace Build_Test_Dashboard.API.Test.Support;

/// <summary>
/// Represents the state of the fake build connection service.
/// </summary>
public class FakeBuildConnectionServiceState
{
    /// <summary>
    /// Gets or sets a value indicating whether to use real service.
    /// </summary>
    /// <value>
    ///   <c>true</c> if use real service; otherwise, <c>false</c>.
    /// </value>
    public bool UseRealService { get; set; }
    /// <summary>
    /// Gets or sets the build connection result.
    /// </summary>
    /// <value>
    /// The build connection result.
    /// </value>
    public BuildConnection? BuildConnectionResult { get; private set; }
    /// <summary>
    /// Gets the build connection list.
    /// </summary>
    /// <value>
    /// The build connection list.
    /// </value>
    public List<BuildConnection>? BuildConnectionList { get; set; }
}
