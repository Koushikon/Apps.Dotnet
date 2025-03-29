namespace WebApi.Client;

public interface IGitHubClient
{
    Task<int> GetFollowersCount();
}