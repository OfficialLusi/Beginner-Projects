using System.Text.Json.Serialization;

namespace GithubUserActivity.Domain.Models;

public class Repo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
