using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Build_Test_Dashboard.Data;

public class SqlServerDashboardDbContextFactory
    : IDesignTimeDbContextFactory<SqlServerDashboardDbContext>
{
    public SqlServerDashboardDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile(
                "appsettings.Development.json",
                optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            configuration["Database:ConnectionString"]
            ?? throw new InvalidOperationException(
                "Database connection string is not configured.");

        var options = new DbContextOptionsBuilder<SqlServerDashboardDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new SqlServerDashboardDbContext(options);
    }
}