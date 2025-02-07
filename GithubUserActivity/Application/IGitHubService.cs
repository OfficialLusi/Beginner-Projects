using GithubUserActivity.Domain;

namespace GithubUserActivity.Application;

public interface IGitHubService
{
    IEnumerable<GitHubEvent> GetUserEvents(string username);
}
