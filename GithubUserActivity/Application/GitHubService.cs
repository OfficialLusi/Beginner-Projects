using GithubUserActivity.Domain;
using GithubUserActivity.Infrastructure;
using System.Runtime.CompilerServices;

namespace GithubUserActivity.Application;

public class GitHubService : IGitHubService
{
    private readonly GitHubApiClient _gitHubApiClient;

    public GitHubService(GitHubApiClient gitHubApiClient)
    {
        _gitHubApiClient = gitHubApiClient;
    }

    public IEnumerable<GitHubEvent> GetUserEvents(string username)
    {
        return _gitHubApiClient.FetchUserEvents(username);
    }
}
