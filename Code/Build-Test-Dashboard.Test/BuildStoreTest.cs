using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Stores;
using Build_Test_Dashboard.Test.Support;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Test
{
    public class BuildStoreTest
    {
        /// <summary>
        /// Gets all asynchronous returns repository builds.
        /// </summary>
        [Fact]
        public async Task GetAllAsync_ReturnsRepositoryBuilds()
        {
            // Arrange
            // Create test context and add builds for repositories 1 and 2.
            await using var context = CreateContext();

            var repositoryStore = new RepositoryStore(context);

            var buildConnectionStore = new BuildConnectionStore(context);

            var buildStore = new BuildStore(context);
            await TestData.AddData(
                repositoryStore,
                buildConnectionStore,
                buildStore,
                TestContext.Current.CancellationToken);

            // Act
            var builds = await buildStore.GetAllAsync(
                1,
                TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(2, builds.Count());
            Assert.All(
                builds,
                build => Assert.Equal(1, build.SourceRepositoryId));
        }

        /// <summary>
        /// Gets asynchronous returns a repository build.
        /// </summary>
        [Fact]
        public async Task GetAsync_ReturnsRepositoryBuild()
        {
            // Arrange
            // Create test context and add builds for repositories 1 and 2.
            await using var context = CreateContext();

            var repositoryStore = new RepositoryStore(context);

            var buildConnectionStore = new BuildConnectionStore(context);

            var buildStore = new BuildStore(context);
            await TestData.AddData(
                repositoryStore,
                buildConnectionStore,
                buildStore,
                TestContext.Current.CancellationToken);

            // Act
            var build = await buildStore.GetAsync(
                4,
                TestContext.Current.CancellationToken);

            // Assert
            Assert.NotNull(build);
            Assert.Equal(4, build.Id);
            Assert.Equal("1001", build.BuildNumber);
        }

        /// <summary>
        /// Deletes a build asynchronously.
        /// </summary>
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            // Create test context and add builds for repositories 1 and 2.
            await using var context = CreateContext();

            var repositoryStore = new RepositoryStore(context);

            var buildConnectionStore = new BuildConnectionStore(context);

            var buildStore = new BuildStore(context);
            await TestData.AddData(
                repositoryStore,
                buildConnectionStore,
                buildStore,
                TestContext.Current.CancellationToken);

            // Act
            var result = await buildStore.DeleteAsync(
                4,
                TestContext.Current.CancellationToken);

            var build = await buildStore.GetAsync(
                4,
                TestContext.Current.CancellationToken);

            var result2 = await buildStore.DeleteAsync(
                4,
                TestContext.Current.CancellationToken);

            // Assert
            Assert.Null(build);
            Assert.True(result);
            Assert.False(result2);
        }

        /// <summary>
        /// Creates the context.
        /// </summary>
        /// <returns></returns>
        private static DashboardDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DashboardDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            var context = new DashboardDbContext(options);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}