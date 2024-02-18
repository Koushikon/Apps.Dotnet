using Refit;

namespace WebApi.Client;

public interface IRefitGitHubClient
{
    [Get("/repos/koushikon/Social.Forum/branches")]
    Task<IEnumerable<object>> GetRepoBranches();
}