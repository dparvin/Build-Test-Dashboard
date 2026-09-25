using Build_Test_Dashboard.Configuration;
using Build_Test_Dashboard.Data;
using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Providers;
using Build_Test_Dashboard.Services;
using Build_Test_Dashboard.Stores;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var (provider, connectionString) = DatabaseConfiguration.GetDatabaseProvider(builder.Configuration);

builder.Services.AddDbContext<DashboardDbContext>(options =>
{
    switch (provider)
    {
        case DatabaseProvider.Sqlite:
            options.UseSqlite(connectionString,
                    sqliteOptions =>
                    {
                        sqliteOptions.MigrationsAssembly(
                            typeof(Build_Test_Dashboard.Migrations.Sqlite.InitialCreate).Assembly.FullName);
                    });
            break;

        case DatabaseProvider.SqlServer:
            options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(
                            typeof(Build_Test_Dashboard.Migrations.SqlServer.InitialCreate).Assembly.FullName);
                    });
            break;

        default:
            throw new InvalidOperationException(
                $"Unsupported database provider: {provider}");
    }
});

builder.Services.AddControllers();

builder.Services.AddHttpClient<GitHubRepositoryProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.github.com/");
});
builder.Services.AddHttpClient<AzureDevOpsRepositoryProvider>(client =>
{
    client.BaseAddress = new Uri("https://dev.azure.com/");
});

builder.Services.AddScoped<IRepositoryProvider>(
    provider => provider.GetRequiredService<GitHubRepositoryProvider>());

builder.Services.AddScoped<IRepositoryProvider>(
    provider => provider.GetRequiredService<AzureDevOpsRepositoryProvider>());

builder.Services.AddScoped<IWindowsCredentialManager, WindowsCredentialManager>();
builder.Services.AddScoped<IRepositoryStore, RepositoryStore>();
builder.Services.AddScoped<ICredentialStore, WindowsCredentialStore>();

builder.Services.AddScoped<IRepositoryService, RepositoryService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

