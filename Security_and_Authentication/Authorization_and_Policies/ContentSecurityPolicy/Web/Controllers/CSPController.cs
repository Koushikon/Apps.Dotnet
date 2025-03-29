using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class CSPController : Controller
{
    private readonly ILogger<CSPController> _logger;

    public CSPController(ILogger<CSPController> logger)
    {
        _logger = logger;
    }

    [HttpPost("csp-violations")]
    public IActionResult CspReport([FromBody] CspViolation cspViolation)
    {
        _logger.LogWarning($"URI: {cspViolation?.CstReport?.DocumentUri}, Blocked: {cspViolation?.CstReport?.BlockedUri}");
        return Ok();
    }
}