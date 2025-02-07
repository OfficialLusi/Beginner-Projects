namespace NumberGuessingGame.obsolete.Domain;

public class GuessingGameManager
{

    private readonly IGuessingGameRepository _repository;
    private readonly List<Game> _games;

    public GuessingGameManager(IGuessingGameRepository repository)
    {
        _repository = repository;
        _games = _repository.LoadGames();
    }

    // the game starts
    public Game StartGame()
    {
        Game game = new Game();
        int newId = _games.Count > 0 ? _games.Max(x => x.Id) + 1 : 1;
        int lives = ManageGameMode();

        game.Id = newId;
        game.Lives = lives;
        game.StartedAt = DateTime.Now;

        _games.Add(game);
        return game;
    }

    // the game will be saved and then stopped
    public void StopGame(int id)
    {
        try
        {
            Game game = _games.FirstOrDefault(x => x.Id == id);
            if (game == null)
            {
                Console.WriteLine($"Game with id: {id} not found.");
                return;
            }

            game.FinishedAt = DateTime.Now;

            var maxLivesByMode = new Dictionary<string, int>
        {
            { "easy", 10 },
            { "medium", 8 },
            { "hard", 5 }
        };

            if (maxLivesByMode.TryGetValue(game.GameMode, out int maxLives))
                game.Attempts = game.Lives > 0 ? maxLives - game.Lives : maxLives;
            else
            {
                Console.WriteLine($"Unknown game mode: {game.GameMode}");
                return;
            }

            if (game.SavedAt != null && game.RestoredAt != null)
                game.GameLastedFor = game.FinishedAt - game.RestoredAt + (game.SavedAt - game.StartedAt);
            else
                game.GameLastedFor = game.FinishedAt - game.StartedAt;

            _repository.SaveGames(_games);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while stopping the game. Exception: {ex.Message}");
        }
    }


    // the game will be saved (and paused)
    public void SaveGame(int id)
    {
        try
        {
            Game game = _games.FirstOrDefault(x => x.Id == id);
            game.SavedAt = DateTime.Now;
            _repository.SaveGames(_games);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Game with id: {id} not found. Exception : {ex.Message}");
        }
    }

    // part of another project

    // the game will be restored (or unpaused) 
    public Game RestoreGame()
    {
        Console.WriteLine("Games not finished: ");
        try
        {
            Console.WriteLine("Games not finished: ");

            foreach (Game game in _games.Where(x => x.SavedAt != null && x.RestoredAt == null))
                Console.WriteLine($"Game Id: {game.Id}\n\tStarted At: {game.StartedAt}\n\tRemaining Lives: {game.Lives}\n\tCurrent Game Time: {game.SavedAt - game.StartedAt}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There are not games unfinished. Exception: {ex.Message}");
            return new Game();
        }

        while (true)
        {
            Console.WriteLine("Insert the id of the game you want to restore: ");
            string? selection = Console.ReadLine();
            if (int.TryParse(selection, out int gameSelected))
            {
                if (gameSelected != null && _games.Any(x => x.Id == gameSelected))
                    return _games.FirstOrDefault(x => x.Id == gameSelected);
                continue;
            }
        }
    }

    public void ShowAllGames()
    {
        foreach (Game game in _games)
        {
            if (game.Result)
                Console.WriteLine($"Game: {game.Id}\n\n" +
                                  $"Game mode: {game.GameMode}\n" +
                                   "Result: you won\n" +
                                  $"Attempts: {game.Attempts}\n" +
                                  $"Started on: {game.StartedAt.Day}/{game.StartedAt.Month}/{game.StartedAt.Year} at {game.StartedAt.Hour}:{game.StartedAt.Minute}:{game.StartedAt.Second}\n" +
                                  $"Ended on: {game.FinishedAt.Day}/{game.FinishedAt.Month}/{game.FinishedAt.Year} at {game.FinishedAt.Hour}:{game.FinishedAt.Minute}:{game.FinishedAt.Second}\n" +
                                  $"Game lasted for: {game.GameLastedFor}\n");
            else
                Console.WriteLine($"Game: {game.Id}\n\n" +
                                  $"Game mode: {game.GameMode}\n" +
                                   "Result: you lost\n" +
                                  $"Attempts: {game.Attempts}\n" +
                                  $"Started on: {game.StartedAt.Day}/{game.StartedAt.Month}/{game.StartedAt.Year} at {game.StartedAt.Hour}:{game.StartedAt.Minute}:{game.StartedAt.Second}\n" +
                                  $"Ended on: {game.FinishedAt.Day}/{game.FinishedAt.Month}/{game.FinishedAt.Year} at {game.FinishedAt.Hour}:{game.FinishedAt.Minute}:{game.FinishedAt.Second}\n" +
                                  $"Game lasted for: {game.GameLastedFor}\n");
        }
    }


    private static int ManageGameMode()
    {
        string? gameMode = null;
        int lives;
        while (true)
        {
            Console.WriteLine("Insert a game mode (easy, medium, hard): ");
            gameMode = Console.ReadLine()?.ToLower();
            switch (gameMode)
            {
                case "easy": lives = 10; break;
                case "medium": lives = 7; break;
                case "hard": lives = 5; break;
                default:
                    {
                        Console.WriteLine("Incorrect game mode inserted. Please enter 'easy', 'medium', or 'hard'.");
                        continue;
                    }
            }
            break;
        }
        Console.WriteLine($"Game mode set to {gameMode}. You have {lives} lives.");
        return lives;
    }


}
