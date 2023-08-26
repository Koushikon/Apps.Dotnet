using AspNetCoreRateLimit;
using APIProtection.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add Caching service
builder.Services.AddResponseCaching();

/**
 * Add IP Rate limiting
 * Source: https://github.com/stefanprodan/AspNetCoreRateLimit
 */

// needed to store rate limit counters and ip rules per server
builder.Services.AddMemoryCache();
// add Rate limiting services through ServicesConfig class
builder.AddRateLimitServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use Caching after UseHttpsRedirection
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

// IP Rate Limiting
app.UseIpRateLimiting();

app.Run();
