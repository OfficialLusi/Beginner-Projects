using System.Text.Json.Serialization;

namespace GithubUserActivity.Domain.Models;

public class Commit
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
