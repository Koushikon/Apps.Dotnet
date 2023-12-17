using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public AuthController(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }


    /***
     * ApiKey with Endpoint Url Query Parameter
     * 200 Successful: https://localhost:44326/api/Auth/AuthViaQueryParam?apiKey=6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN
     * 401 Unauthorized: https://localhost:44326/api/Auth/AuthViaQueryParam?apiKey=test
     * 400 Bad Request: https://localhost:44326/api/Auth/AuthViaQueryParam?apiKey=
     */
    [HttpGet("AuthViaQueryParam")]
    public IActionResult AuthViaQueryParam(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return BadRequest();
        }

        bool isValid = _apiKeyValidation.IsValidKey(apiKey);

        if (!isValid)
        {
            return Unauthorized();
        }
        return Ok();
    }


    /***
     * ApiKey with Http Request Body
     * 200 Successful: https://localhost:44326/api/Auth/AuthViaReqBody
     * Body:
     * {
     *   "apiKey": "6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN"
     * }
     * 
     * 401 Unauthorized: https://localhost:44326/api/Auth/AuthViaReqBody
     * Body:
     * {
     *   "apiKey": "test"
     * }
     * 
     * 400 Bad Request: https://localhost:44326/api/Auth/AuthViaReqBody
     * Body:
     * {
     *   "apiKey": ""
     * }
     */
    [HttpPost("AuthViaReqBody")]
    public IActionResult AuthViaReqBody([FromBody] AuthModel authModel)
    {
        if (string.IsNullOrWhiteSpace(authModel.ApiKey))
        {
            return BadRequest();
        }

        bool isValid = _apiKeyValidation.IsValidKey(authModel.ApiKey);

        if (!isValid)
        {
            return Unauthorized();
        }
        return Ok();
    }


    /***
     * ApiKey with Http Request Custom Header: "X-API-KEY"
     * Desc: We include the API key in the header, such as X-API-Key.
     * We can then retrieve the API key from the request headers for authentication and authorization.
     * 
     * 200 Successful: https://localhost:44326/api/Auth/AuthViaReqHeader
     * X-API-KEY: 6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN
     * 
     * 401 Unauthorized: https://localhost:44326/api/Auth/AuthViaReqHeader
     * X-API-KEY: test
     * 
     * 400 Bad Request: https://localhost:44326/api/Auth/AuthViaReqHeader
     * X-API-KEY: 
     */
    [HttpGet("AuthViaReqHeader")]
    public IActionResult AuthViaReqHeader()
    {
        string? apiKey = Request.Headers[Constants.ApiKeyHeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return BadRequest();
        }

        bool isValid = _apiKeyValidation.IsValidKey(apiKey);

        if (!isValid)
        {
            return Unauthorized();
        }
        return Ok();
    }
}
