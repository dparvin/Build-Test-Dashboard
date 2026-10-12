using Build_Test_Dashboard.Test.Support;

namespace Build_Test_Dashboard.Test;

public class BuildConnectionServiceTests
{
    /// <summary>
    /// The factory
    /// </summary>
    private readonly TestWebApplicationFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildConnectionServiceTests"/> class.
    /// </summary>
    public BuildConnectionServiceTests()
    {
        factory = new TestWebApplicationFactory();
    }
}