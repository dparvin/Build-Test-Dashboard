using Build_Test_Dashboard.Controllers;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="BuildsController"/> class in the build and test dashboard.
/// </summary>
public class BuildsControllerTests
{
    /// <summary>
    /// Test that Get returns a build.
    /// </summary>
    [Fact]
    public void Get_ReturnsBuild()
    {

        var controller = new BuildsController();
        var result = controller.Get(1);

        Assert.NotNull(result);
    }

    /// <summary>
    /// Test that GetTests returns a list of Test Runs.
    /// </summary>
    [Fact]
    public void Get_ReturnsTests()
    {

        var controller = new BuildsController();
        var result = controller.GetTests(1);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}