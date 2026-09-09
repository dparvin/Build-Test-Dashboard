using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Providers;
using Build_Test_Dashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRepositoryProvider, GitHubRepositoryProvider>();
builder.Services.AddScoped<IRepositoryProvider, AzureDevOpsRepositoryProvider>();

builder.Services.AddScoped<RepositoryService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
