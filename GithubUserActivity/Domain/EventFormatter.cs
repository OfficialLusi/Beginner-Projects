namespace GithubUserActivity.Domain;

public class EventFormatter
{

    public static string FormatEvent(GitHubEvent gitEvent)
    {
        return gitEvent.Type switch
        {
            "PushEvent" => $"Pushed {gitEvent.Payload.Commits.Length} commits to {gitEvent.Repo.Name}",
            "IssuesEvent" => $"{gitEvent.Payload.Action} a new issue in {gitEvent.Repo.Name}",
            "WatchEvent" => $"Starred {gitEvent.Repo.Name}",
            _ => $"{gitEvent.Type} event in {gitEvent.Repo.Name}"
        };
    }

}
