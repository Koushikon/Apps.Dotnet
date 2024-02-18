using Microsoft.Net.Http.Headers;

namespace WebApi.Client;

public class GitHubClient : IGitHubClient
{
    private readonly HttpClient _client;

    public GitHubClient(HttpClient client)
    {
        _client = client;
        ConfigureClient();
    }

    public async Task<int> GetFollowersCount()
    {
        var httpResponse = await _client.GetAsync("users/koushikon/followers");
        var contentResponse = await httpResponse.Content.ReadFromJsonAsync<object[]>();

        return contentResponse?.Length ?? 0;
    }

    private void ConfigureClient()
    {
        _client.BaseAddress = new Uri("https://api.github.com/");
        _client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        _client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");
    }
}