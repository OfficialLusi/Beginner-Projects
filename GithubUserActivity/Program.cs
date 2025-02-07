using GithubUserActivity.Application;
using GithubUserActivity.Domain;
using GithubUserActivity.Infrastructure;

namespace GithubUserActivity;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Insert the username of the user you want to knwo the activity: ");

        string username = Console.ReadLine();

        Console.WriteLine($"Usage: github-activity of {username}\n\n");
        
        IGitHubService gitHubService = new GitHubService(new GitHubApiClient());

        try
        {
            var events = gitHubService.GetUserEvents(username);

            foreach (var gitEvent in events)
            {
                Console.WriteLine(EventFormatter.FormatEvent(gitEvent));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}