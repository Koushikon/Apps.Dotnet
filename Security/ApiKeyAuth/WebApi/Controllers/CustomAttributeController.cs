using Microsoft.AspNetCore.Mvc;
using WebApi.CustomAttributes;

namespace WebApi.Controllers;


// [ApiKey] It can be used in the Controller or Action level.

// [ApiKey]
[Route("api/[controller]")]
[ApiController]
public class CustomAttributeController : ControllerBase
{

    /***
     * [ApiKey]
     * This Attribute is a custom attribute,
     * It can be used in the Controller or Action level.
     * 
     * * Request:
     * Desc: We include the API key in the header, such as X-API-Key.
     * We can then retrieve the API key from the request headers for authentication and authorization.
     * 
     * 200 Successful: https://localhost:44326/api/CustomAttribute/AuthViaAttribute
     * X-API-KEY: 6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN
     * 
     * 401 Unauthorized: https://localhost:44326/api/CustomAttribute/AuthViaAttribute
     * X-API-KEY: test
     * 
     * 400 Bad Request: https://localhost:44326/api/CustomAttribute/AuthViaAttribute
     * X-API-KEY: 
     */
    [ApiKey]
    [HttpGet("AuthViaAttribute")]
    public IActionResult AuthViaAttribute()
    {
        return Ok();
    }

    
    [HttpGet("NoAuthViaApiKey")]
    public IActionResult NoAuthViaApiKey()
    {
        return Ok();
    }
}