using Newtonsoft.Json;
using NumberGuessingGame.obsolete.Domain;

namespace NumberGuessingGame.obsolete.Infrastructure;

public class GuessingGameRepository : IGuessingGameRepository
{
    private readonly string _filePath;

    public GuessingGameRepository(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
            CreateEmptyJsonFile();
    }

    public List<Game> LoadGames()
    {
        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Game>>(json) ?? new List<Game>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the message: {ex.Message}");
            return new List<Game>();
        }

    }

    public void SaveGames(List<Game> games)
    {
        try
        {
            string json = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the message: {ex.Message}");
        }
    }

    #region private methods
    private void CreateEmptyJsonFile()
    {
        File.WriteAllText(_filePath, "[]");
    }
    #endregion
}
