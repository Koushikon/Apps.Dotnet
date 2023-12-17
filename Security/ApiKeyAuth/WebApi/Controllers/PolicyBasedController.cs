using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolicyBasedController : ControllerBase
{
    /***
     * Authorize(Policy = "ApiKeyPolicy")]
     * The [Authorize] attribute with Policy = "ApiKeyPolicy" ensures that only requests
     * with a valid API key will be authorized to access this endpoint.
     * 
     * * Request:
     * Desc: We include the API key in the header, such as X-API-Key.
     * We can then retrieve the API key from the request headers for authentication and authorization.
     * 
     * 200 Successful: https://localhost:44326/api/PolicyBased
     * X-API-KEY: 6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN
     * 
     * 401 Unauthorized: https://localhost:44326/api/PolicyBased
     * X-API-KEY: test
     * 
     * 401 Unauthorized: https://localhost:44326/api/PolicyBased
     * No Header
     */
    [HttpGet]
    [Authorize(Policy = "ApiKeyPolicy")]
    public IActionResult Get()
    {
        return Ok();
    }
}