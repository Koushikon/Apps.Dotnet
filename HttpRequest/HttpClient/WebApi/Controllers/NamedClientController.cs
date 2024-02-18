using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NamedClientController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;

    public NamedClientController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetGitHubFollowers()
    {
        var client = _clientFactory.CreateClient("GitHub");

        var httpResponse = await client.GetAsync("users/koushikon/followers");
        var contentResponse = await httpResponse.Content.ReadAsStringAsync();

        return Ok(contentResponse);
    }
}