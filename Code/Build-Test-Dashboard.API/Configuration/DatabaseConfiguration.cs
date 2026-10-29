using Build_Test_Dashboard.Contracts.Enums;

namespace Build_Test_Dashboard.API.Configuration
{
    /// <summary>
    /// A static class that provides methods for configuring the database connection.
    /// </summary>
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Gets the database provider.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>A tuple containing the database provider and the connection string.</returns>
        public static (DatabaseProvider, string?) GetDatabaseProvider(
            IConfiguration configuration)
        {
            var database = configuration.GetSection("Database");

            var provider = Enum.Parse<DatabaseProvider>(database["Provider"] ?? "sqlite", true);
            var connectionString = database["ConnectionString"];

            return (provider, connectionString);
        }

    }
}
