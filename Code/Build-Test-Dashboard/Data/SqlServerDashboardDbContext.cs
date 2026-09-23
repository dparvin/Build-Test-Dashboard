using Microsoft.EntityFrameworkCore;

namespace Build_Test_Dashboard.Data;

public class SqlServerDashboardDbContext(
    DbContextOptions<SqlServerDashboardDbContext> options)
    : DashboardDbContext(options)
{
}