using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

public class SqliteDashboardDbContext(
    DbContextOptions<SqliteDashboardDbContext> options)
    : DashboardDbContext(options)
{
}