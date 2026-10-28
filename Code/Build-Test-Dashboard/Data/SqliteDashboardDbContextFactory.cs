using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Build_Test_Dashboard.Data;

/// <summary>
/// Provides a factory for creating instances of <see cref="SqliteDashboardDbContext"/> at 
/// design time, enabling Entity Framework Core tools to work with the context.
/// </summary>
/// <seealso cref="IDesignTimeDbContextFactory&lt;SqliteDashboardDbContext&gt;" />
public class SqliteDashboardDbContextFactory
    : IDesignTimeDbContextFactory<SqliteDashboardDbContext>
{
    /// <summary>
    /// Creates a new instance of a derived context.
    /// </summary>
    /// <param name="args">Arguments provided by the design-time service.</param>
    /// <returns>
    /// An instance of <typeparamref name="TContext" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">Database connection string is not configured.</exception>
    public SqliteDashboardDbContext CreateDbContext(string[] args)
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

        var options = new DbContextOptionsBuilder<SqliteDashboardDbContext>()
            .UseSqlite(connectionString)
            .Options;

        return new SqliteDashboardDbContext(options);
    }
}