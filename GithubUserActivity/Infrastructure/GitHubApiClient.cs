using GithubUserActivity.Domain;
using System.Net;
using System.Text.Json;

namespace GithubUserActivity.Infrastructure;

public class GitHubApiClient
{
    public GitHubEvent[] FetchUserEvents(string username)
    {
        string url = $"https://api.github.com/users/{username}/events";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.UserAgent = "GitHubActivityApp";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            string jsonResponse = reader.ReadToEnd();
            return JsonSerializer.Deserialize<GitHubEvent[]>(jsonResponse);
        }

    }

}
