using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SampleClientController : ControllerBase
{
    // private static readonly HttpClient _client = new();
    private readonly HttpClient _client = new();

    public SampleClientController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetGitHubFollowers()
    {
        _client.BaseAddress = new Uri("https://api.github.com/");
        _client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        _client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");

        var httpResponse = await _client.GetAsync("users/koushikon/followers");
        var contentResponse = await httpResponse.Content.ReadAsStringAsync();

        return Ok(contentResponse);
    }
}