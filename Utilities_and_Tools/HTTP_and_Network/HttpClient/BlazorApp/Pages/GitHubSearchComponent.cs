using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages;

public class GitHubSearchComponent : ComponentBase
{
    public string AccountName { get; set; } = string.Empty;

    public string AccountInfo { get; set; } = string.Empty;

    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; }

    public async Task LoadAccount()
    {
        var client = HttpClientFactory.CreateClient("GitHub");

        AccountInfo = await client.GetStringAsync($"users/{AccountName}");
    }
}