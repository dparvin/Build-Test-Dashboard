using Build_Test_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

public class DashboardDbContext(
    DbContextOptions<DashboardDbContext> options)
    : DbContext(options)
{
    public DbSet<Repository> Repositories => Set<Repository>();
    public DbSet<Build> Builds => Set<Build>();
    public DbSet<TestRun> TestRuns => Set<TestRun>();
}