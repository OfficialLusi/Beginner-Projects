using System.Reflection.Metadata;

namespace NumberGuessingGame.Domain;

public class GameManager
{
    private readonly IGamesRepo _gamesRepo;
    private readonly List<Game> _games;

    private const int easy = 10;
    private const int medium = 8;
    private const int hard = 5;

    public GameManager(IGamesRepo gamesRepo)
    {
        _gamesRepo = gamesRepo;
        _games = _gamesRepo.LoadGames();
    }

    public Game StartGame()
    {
        Game game = new Game()
        {
            Id = _games.Count() > 0 ? _games.Max(x => x.Id) + 1: 1,
            StartDate = DateTime.Now
        };
        ManageGameMode(game);
        return game;
    }

    public void StopGame(Game game)
    {
        switch (game.Mode)
        {
            case Game.GameMode.Easy: game.Attempts = game.Lives != 0 ? easy - game.Lives : easy; break;
            case Game.GameMode.Medium: game.Attempts = game.Lives != 0 ? medium - game.Lives : medium; break;
            case Game.GameMode.Hard: game.Attempts = game.Lives != 0 ? hard - game.Lives : hard; break;
        }
        game.EndDate = DateTime.Now;
        game.GameDuration = game.EndDate - game.StartDate;
        if(game.Attempts < _games.Min(x => x.Attempts))
            Console.WriteLine($"You set a new attempts record: {game.Attempts} for {game.Mode} mode");
        if (game.GameDuration < _games.Min(x => x.GameDuration))
            Console.WriteLine($"You set a new time record: {game.GameDuration} for {game.Mode} mode");
        _games.Add(game);
        _gamesRepo.SaveGames(_games);
    }

    public void ShowAllGames()
    {
        if(_games.Count == 0)
        {
            Console.WriteLine("There's no games played.");
            return;
        }

        foreach (var game in _games)
        {
            if (game.Result)
                Console.WriteLine($"Game Id: {game.Id}\n" +
                                  $"Game Mode: {game.Mode}\n" +
                                  $"Attempts: {game.Attempts}\n" +
                                  $"Result: you won\n" +
                                  $"Game Started on: {game.StartDate}\n" +
                                  $"Game Ended on: {game.EndDate}\n" +
                                  $"Game Duration: {game.GameDuration}\n");
            else
                Console.WriteLine($"Game Id: {game.Id}\n" +
                                  $"Game Mode: {game.Mode}\n" +
                                  $"Attempts: {game.Attempts}\n" +
                                  $"Result: you lost\n" +
                                  $"Game Started on: {game.StartDate}\n" +
                                  $"Game Ended on: {game.EndDate}\n" +
                                  $"Game Duration: {game.GameDuration}\n");
        }
    }

    public void ShowAllGamesWon()
    {
        List<Game> gamesWon = _games.Where(game => game.Result == true).ToList();

        if (gamesWon.Count == 0)
        {
            Console.WriteLine("There's no games won.");
            return;
        }

        foreach (var game in gamesWon)
            Console.WriteLine($"Game Id: {game.Id}\n" +
                              $"Game Mode: {game.Mode}\n" +
                              $"Attempts: {game.Attempts}\n" +
                              $"Game Started on: {game.StartDate.Date} at {game.StartDate.TimeOfDay}\n" +
                              $"Game Ended on: {game.EndDate.Date} at {game.EndDate.TimeOfDay}\n" +
                              $"Game Duration: {game.GameDuration}");
    }

    public void ShowAllGamesLost()
    {
        List<Game> gamesLost = _games.Where(game => game.Result == false).ToList();

        if (gamesLost.Count == 0)
        {
            Console.WriteLine("There's no games lost.");
            return;
        }

        foreach (var game in gamesLost)
            Console.WriteLine($"Game Id: {game.Id}\n" +
                              $"Game Mode: {game.Mode}\n" +
                              $"Attempts: {game.Attempts}\n" +
                              $"Game Started on: {game.StartDate.Date} at {game.StartDate.TimeOfDay}\n" +
                              $"Game Ended on: {game.EndDate.Date} at {game.EndDate.TimeOfDay}\n" +
                              $"Game Duration: {game.GameDuration}");
    }
    public void ShowGameById(int id)
    {
        Game? game = null;

        if(_games.Any(game => game.Id == id))
            game = _games.FirstOrDefault(x => x.Id == id);

        if (game == null) 
        {
            Console.WriteLine($"No game found with id: {id}");
            return;
        }

        if (game.Result)
            Console.WriteLine($"Game Id: {game.Id}\n" +
                              $"Game Mode: {game.Mode}\n" +
                              $"Attempts: {game.Attempts}\n" +
                              $"Result: you won\n" +
                              $"Game Started on: {game.StartDate.Date} at {game.StartDate.TimeOfDay}\n" +
                              $"Game Ended on: {game.EndDate.Date} at {game.EndDate.TimeOfDay}\n" +
                              $"Game Duration: {game.GameDuration}");
        else
            Console.WriteLine($"Game Id: {game.Id}\n" +
                              $"Game Mode: {game.Mode}\n" +
                              $"Attempts: {game.Attempts}\n" +
                              $"Result: you lost\n" +
                              $"Game Started on: {game.StartDate.Date} at {game.StartDate.TimeOfDay}\n" +
                              $"Game Ended on: {game.EndDate.Date} at {game.EndDate.TimeOfDay}\n" +
                              $"Game Duration: {game.GameDuration}");
    }

    public void DeleteAll()
    {
        _games.Clear();
        _gamesRepo.DeleteGames(_games);
        Console.WriteLine("Games deleted correctly.");
    }

    private static void ManageGameMode(Game game)
    {
        while (true)
        {
            Console.WriteLine("Insert a game mode (easy, medium, hard): ");
            string gameMode = Console.ReadLine()?.ToLower();
            switch (gameMode)
            {
                case "easy":
                    {
                        game.Lives = 10;
                        game.Mode = Game.GameMode.Easy;
                    }; break;
                case "medium":
                    {
                        game.Lives = 7;
                        game.Mode = Game.GameMode.Medium;

                    }; break;
                case "hard":
                    {
                        game.Lives = 5;
                        game.Mode = Game.GameMode.Hard;
                    }; break;
                default:
                    {
                        Console.WriteLine("Incorrect game mode inserted. Please enter 'easy', 'medium', or 'hard'.");
                        continue;
                    }
            }
            break;
        }
        Console.WriteLine($"Game mode set to {game.Mode}. You have {game.Lives} lives.");
    }
}
