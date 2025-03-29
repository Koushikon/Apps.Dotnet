using Microsoft.Extensions.Options;

namespace WebApi.Options;

/***
 * Our Database options will be resolved only once, The first time its injected
 * We can't change the values at runtime
 * To apply changes we have to restart the application
 */

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "DatabaseOptions";
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        string connectionString = _configuration.GetConnectionString("Database") ?? string.Empty;

        options.ConnectionString = connectionString;    // Configure Database Connection String
        _configuration.GetSection(ConfigurationSectionName).Bind(options);  // Configure Database Connection Options
    }
}