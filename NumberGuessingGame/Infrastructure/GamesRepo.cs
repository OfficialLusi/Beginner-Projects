using Newtonsoft.Json;
using NumberGuessingGame.Domain;

namespace NumberGuessingGame.Infrastructure;

public class GamesRepo : IGamesRepo
{
    private readonly string _filePath;

    public GamesRepo(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
            CreateEmptyJsonFile();
    }

    public List<Game> LoadGames()
    {
        string json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Game>>(json) ?? new List<Game>();
    }

    public void SaveGames(List<Game> games)
    {
        string json = JsonConvert.SerializeObject(games, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }
    public void DeleteGames(List<Game> games)
    {
        string json = JsonConvert.SerializeObject(games, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    private void CreateEmptyJsonFile()
    {
        File.WriteAllText(_filePath, "[]");
    }

}
