using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoggingDemo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        /**
         * * Different Logging Levels
         * * UMSG = User Message
         */
        _logger.LogTrace("UMSG: This is a Trace log.");
        //_logger.LogDebug("UMSG: This is a Debug log.");
        //_logger.LogInformation("UMSG: This is a Information log.");
        //_logger.LogWarning("UMSG: This is a Warning log.");
        //_logger.LogError("UMSG: This is a Error log.");
        //_logger.LogCritical("UMSG: This is a Critical log.");


        // Logging with Event Id
        // Used different overload
        //_logger.LogWarning(LoggingId.EventId, "UMSG: This is a Warning log.");

        // Logging with Exact DateTime
        // Best to use String Composite Formatting for several reason.
        //_logger.LogError("UMSG: From Index Page Error log at{Time}", DateTime.UtcNow);

        // Logging with a Real-life scenario
        try
        {
            throw new Exception("Go to Catch Block.");
        }
        catch(Exception ex)
        {
            //_logger.LogInformation(ex, "UMSG: exception Info at {Time}", DateTime.UtcNow);
            //_logger.LogWarning(ex, "UMSG: exception Warning at {Time}", DateTime.UtcNow);
            //_logger.LogError(ex, "UMSG: exception Error at {Time}", DateTime.UtcNow);
            _logger.LogCritical(ex, "UMSG: exception Critical at {Time}", DateTime.UtcNow);
        }
    }
}

public class LoggingId
{
    public const int EventId = 1001;
}