using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

public class SqlServerDashboardDbContext(
    DbContextOptions<SqlServerDashboardDbContext> options)
    : DashboardDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuildConnection>(entity =>
        {
            entity.Property(connection => connection.Provider)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.Property(connection => connection.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });
    }
}