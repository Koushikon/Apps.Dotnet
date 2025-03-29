using APIMonitor.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

/**
 * * HealthCheck Info: https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
 * * Watchdog Monitor Info: https://github.com/IzyPro/WatchDog
 */

// Add services to the container.
// Health checks Service
builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Website Health Check")
    .AddCheck<RandomHealthCheck>("Database Health Check");

// Watchdog Monitor Service
builder.Services.AddWatchDogServices();

builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health");
    opts.SetEvaluationTimeInSeconds(5); // For production Best to use 1 minute
    opts.SetMinimumSecondsBetweenFailureNotifications(10); // duration of Fail health check report, Best to use 5 minute
}).AddInMemoryStorage();    // Can be stored in database

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// For WatchDog Monitor API
app.UseWatchDogExceptionLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// For Healthchecks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// To Allow Anonymous to access health checks
// app.MapHealthChecks("/health").AllowAnonymous();

app.MapHealthChecksUI();

// For WatchDog Monitor API
app.UseWatchDog(opts =>
{
    opts.WatchPageUsername = app.Configuration.GetValue<string>("WatchDog:Username");
    opts.WatchPagePassword = app.Configuration.GetValue<string>("WatchDog:Password");
    opts.Blacklist = "health"; // Do not watch for this URL
});

app.Run();
