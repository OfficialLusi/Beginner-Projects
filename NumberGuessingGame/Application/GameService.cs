using NumberGuessingGame.Domain;

namespace NumberGuessingGame.Application;

public class GameService : IGameService
{

    private readonly GameManager _manager;

    public GameService(GameManager manager)
    {
        _manager = manager;
    }

    public Game StartGame()
    {
        try
        {
            return _manager.StartGame();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to start a game through manager: {ex.Message}");
            return new Game();
        }
    }

    public void StopGame(Game game)
    {
        try
        {
            _manager.StopGame(game);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to stop a game through manager: {ex.Message}");
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
            Console.WriteLine($"Impossible to show all games through manager: {ex.Message}");
        }
    }

    public void ShowAllGamesWon()
    {
        try
        {
            _manager.ShowAllGamesWon();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to shoe all games won through manager: {ex.Message}");
        }
    }

    public void ShowAllGamesLost()
    {
        try
        {
            _manager.ShowAllGamesLost();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to shoe all games lost through manager: {ex.Message}");
        }
    }

    public void ShowGameById(int id)
    {
        try
        {
            _manager.ShowGameById(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to show a game by id through manager: {ex.Message}");
        }
    }

    public void DeleteAll()
    {
        try
        {
            _manager.DeleteAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Impossible to delete all games through manager: {ex.Message}");
        }
    }

}
