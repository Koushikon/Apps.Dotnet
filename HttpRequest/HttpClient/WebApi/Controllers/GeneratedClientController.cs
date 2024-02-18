using Microsoft.AspNetCore.Mvc;
using WebApi.Client;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneratedClientController : ControllerBase
{
    private readonly IRefitGitHubClient _client;

    public GeneratedClientController(IRefitGitHubClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetRepoBranches()
    {
        var branches = await _client.GetRepoBranches();

        return Ok(branches);
    }
}