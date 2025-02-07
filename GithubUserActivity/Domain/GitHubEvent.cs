using GithubUserActivity.Domain.Models;
using System.Text.Json.Serialization;

namespace GithubUserActivity.Domain;

public class GitHubEvent
{

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("repo")]
    public Repo Repo{ get; set; }

    [JsonPropertyName("payload")]
    public Payload Payload { get; set; }
}
