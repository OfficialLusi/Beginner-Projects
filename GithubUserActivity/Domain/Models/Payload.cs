using System.Text.Json.Serialization;

namespace GithubUserActivity.Domain.Models;

public class Payload
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("commits")]
    public Commit[] Commits { get; set; }
}
