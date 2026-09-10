using Build_Test_Dashboard.Controllers;

namespace Build_Test_Dashboard.Test;

/// <summary>
/// Represents the unit tests for the <see cref="BuildsController"/> class in the build and test dashboard.
/// </summary>
public class BuildsControllerTests
{
    /// <summary>
    /// Gets the returns build.
    /// </summary>
    [Fact]
    public void Get_ReturnsBuild()
    {

        var controller = new BuildsController();
        var result = controller.Get(1);

        Assert.NotNull(result);
    }

    [Fact]
    public void Get_ReturnsTest()
    {

        var controller = new BuildsController();
        var result = controller.GetTests(1);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
