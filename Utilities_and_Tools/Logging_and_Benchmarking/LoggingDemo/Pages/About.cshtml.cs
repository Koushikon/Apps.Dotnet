using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoggingDemo.Pages;

public class AboutModel : PageModel
{
    private readonly ILogger _logger;

    // With Manually specified Category Name instead of class Name
    public AboutModel(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger("AboutCategory");
    }

    public void OnGet()
    {
        // Logging with Event Id
        // Used different overload
        _logger.LogWarning(LoggingId.EventId, "UMSG: From About This is a Warning log.");
    }
}
