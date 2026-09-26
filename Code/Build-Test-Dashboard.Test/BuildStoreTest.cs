using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Models;
using Build_Test_Dashboard.Stores;
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

            var buildStore = new BuildStore(context);
            await AddData(repositoryStore, buildStore);

            // Act
            var builds = await buildStore.GetAllAsync(
                1,
                TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(2, builds.Count());
            Assert.All(
                builds,
                build => Assert.Equal(1, build.RepositoryId));
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

            var buildStore = new BuildStore(context);
            await AddData(repositoryStore, buildStore);

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

            var buildStore = new BuildStore(context);
            await AddData(repositoryStore, buildStore);

            // Act
            var result = await buildStore.DeleteAsync(
                4,
                TestContext.Current.CancellationToken);

            var build = await buildStore.GetAsync(4, TestContext.Current.CancellationToken);

            var result2 = await buildStore.DeleteAsync(
                4,
                TestContext.Current.CancellationToken);

            // Assert
            Assert.Null(build);
            Assert.True(result);
            Assert.False(result2);
        }

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="repositoryStore">The repository store.</param>
        /// <param name="buildStore">The build store.</param>
        private static async Task AddData(
            RepositoryStore repositoryStore,
            BuildStore buildStore)
        {
            var repository = new Repository
            {
                Id = 1,
                Name = "Build/Test Dashboard",
                Provider = "GitHub",
                Owner = "dparvin",
                Project = "",
                RepositoryName = "Build-Test-Dashboard"
            };
            await repositoryStore.StoreAsync(
                repository,
                TestContext.Current.CancellationToken);


            var build = new Build
            {
                Id = 1,
                RepositoryId = 1,
                BuildNumber = "100",
                Branch = "main",
                Commit = "abc123",
                Status = "Succeeded"
            };
            await buildStore.StoreAsync(
                build,
                TestContext.Current.CancellationToken);

            build = new Build
            {
                Id = 2,
                RepositoryId = 1,
                BuildNumber = "101",
                Branch = "main",
                Commit = "def456",
                Status = "Succeeded"
            };
            await buildStore.StoreAsync(
                build,
                TestContext.Current.CancellationToken);

            repository = new Repository
            {
                Id = 2,
                Name = "PropertyGridHelpers",
                Provider = "GitHub",
                Owner = "dparvin",
                Project = "",
                RepositoryName = "PropertyGridHelpers"
            };
            await repositoryStore.StoreAsync(
                repository,
                TestContext.Current.CancellationToken);


            build = new Build
            {
                Id = 3,
                RepositoryId = 2,
                BuildNumber = "1000",
                Branch = "main",
                Commit = "abc123",
                Status = "Succeeded"
            };
            await buildStore.StoreAsync(
                build,
                TestContext.Current.CancellationToken);

            build = new Build
            {
                Id = 4,
                RepositoryId = 2,
                BuildNumber = "1001",
                Branch = "main",
                Commit = "def456",
                Status = "Succeeded"
            };
            await buildStore.StoreAsync(
                build,
                TestContext.Current.CancellationToken);
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