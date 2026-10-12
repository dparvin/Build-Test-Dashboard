using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

public class SqliteDashboardDbContext(
    DbContextOptions<SqliteDashboardDbContext> options)
    : DashboardDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuildConnection>(entity =>
        {
            entity.Property(connection => connection.Provider)
                .UseCollation("NOCASE");

            entity.Property(connection => connection.Name)
                .UseCollation("NOCASE");
        });
    }
}