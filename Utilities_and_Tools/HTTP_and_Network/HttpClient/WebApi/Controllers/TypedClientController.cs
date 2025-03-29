using Microsoft.AspNetCore.Mvc;
using WebApi.Client;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypedClientController : ControllerBase
{
    private readonly IGitHubClient _client;

    public TypedClientController(IGitHubClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetFollowerCount()
    {
        var count = await _client.GetFollowersCount();
        return Ok(count);
    }
}