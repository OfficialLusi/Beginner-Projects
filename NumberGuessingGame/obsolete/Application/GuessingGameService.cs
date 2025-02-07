using NumberGuessingGame.obsolete.Domain;

namespace NumberGuessingGame.obsolete.Application;

public class GuessingGameService : IGuessingGameService
{

    private readonly GuessingGameManager _manager;

    public GuessingGameService(GuessingGameManager manager)
    {
        _manager = manager;
    }

    public Game StartGame()
    {
        return _manager.StartGame();
    }

    public void StopGame(int id)
    {
        try
        {
            _manager.StopGame(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during stopping the game: {ex.Message}");
        }
    }

    public void SaveGame(int id)
    {
        try
        {
            _manager.SaveGame(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during saving the game: {ex.Message}");
        }
    }

    public void RestoreGame()
    {
        try
        {
            _manager.RestoreGame();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during restoring the game: {ex.Message}");
        }
    }

    public void ShowAllGames()
    {
        try
        {
            _manager.ShowAllGames();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during restoring the game: {ex.Message}");
        }
    }
}
