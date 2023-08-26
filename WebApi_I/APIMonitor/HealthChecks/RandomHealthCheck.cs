using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace APIMonitor.HealthChecks;

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // Threse are things we can check:
        // We can check if the database is available or not.
        // We can check how responsive the website is.
        // We can check to see if certain Endpoint are available or not.
        // We can check to see how long it takes to  get the data from Database.
        // We can check if certtain Web Services are up or not.

        int responseInMS = Random.Shared.Next(300);

        if(responseInMS < 100)
        {
            return Task.FromResult(HealthCheckResult.Healthy($"Response time is excelent, {responseInMS}ms"));
        }
        else if (responseInMS < 200)
        {
            return Task.FromResult(HealthCheckResult.Degraded($"Response time is decreased, {responseInMS}ms"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Response time is unacceptable, {responseInMS}ms"));
        }
    }
}
