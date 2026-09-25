using Build_Test_Dashboard.Enums;

namespace Build_Test_Dashboard.Configuration
{
    public static class DatabaseConfiguration
    {
        public static (DatabaseProvider, string?) GetDatabaseProvider(IConfiguration configuration)
        {
            var database = configuration.GetSection("Database");

            var provider = Enum.Parse<DatabaseProvider>(database["Provider"] ?? "sqlite", true);
            var connectionString = database["ConnectionString"];

            return (provider, connectionString);
        }

    }
}
