using NumberGuessingGame.Domain;
using NumberGuessingGame.Infrastructure;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace NumberGuessingGame.Application;

public class CliService
{

    private readonly string _filePath;
    private readonly IGamesRepo _repo;
    private readonly GameManager _manager;
    private readonly IGameService _service;

    public CliService()
    {
        _filePath = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\NumberGuessingGame\\games.json";
        _repo = new GamesRepo(_filePath);
        _manager = new GameManager(_repo);
        _service = new GameService(_manager);
    }

    public void CliMain()
    {
        var rootCommand = BuildRootCommand();

        Console.WriteLine("\nWelcome to the Number Guessing Game!");
        
        while (true)
        {
            Console.WriteLine("Type '--help' to see available commands or 'exit' to quit.\n");
            Console.WriteLine("Insert a command");

            Console.Write("> ");
            var userInput = Console.ReadLine();
            string[] userArgs = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (userArgs[0].ToLower() == "exit")
            {
                Console.WriteLine("Exiting the application. Goodbye!");
                return;
            }

            rootCommand.Invoke(userArgs);
        }
    }

    private RootCommand BuildRootCommand()
    {
        // define "start" command
        var startCommand = new Command("start-game", "Start a new game");
        startCommand.SetHandler(StartGame);

        // define "show-all" command
        var showAllGamesCommand = new Command("show-all", "Show all games");
        showAllGamesCommand.SetHandler(_service.ShowAllGames);

        // define "show-all" command
        var showAllGamesWonCommand = new Command("show-all-won", "Show all games won");
        showAllGamesWonCommand.SetHandler(_service.ShowAllGamesWon);

        // define "show-all" command
        var showAllGamesLostCommand = new Command("show-all-lost", "Show all games lost");
        showAllGamesLostCommand.SetHandler(_service.ShowAllGamesLost);

        // define "show-all" command
        Option<int> gameById = new Option<int>("--id", "id for showing a game") { IsRequired = true };
        var showGameByIdCommand = new Command("show-game-id", "Show game by id");
        showGameByIdCommand.SetHandler(_service.ShowGameById, gameById);

        // define "delete-all"
        var deleteAllCommand = new Command("delete-all", "Delete all games memory");
        deleteAllCommand.SetHandler(_service.DeleteAll);
        
        // define "exit" command
        var exitCommand = new Command("exit", "Exit application");

        var rootCommand = new RootCommand
            {
                startCommand,
                showAllGamesCommand,
                showAllGamesWonCommand,
                showAllGamesLostCommand,
                deleteAllCommand,
                showGameByIdCommand
            };

        rootCommand.Description = "Number Guessing Game CLI - Manage and play your guessing games.";

        return rootCommand;
    }

    private void StartGame()
    {
        Game game = _service.StartGame();
        PlayGame(game);
    }
    
    private void PlayGame(Game game)
    {
        Random random = new Random();
        int targetNumber = random.Next(1, 101);
        Console.WriteLine("I'm thinking of a number between 1 and 100. Try to guess it!");

        while (game.Lives > 0)
        {
            Console.WriteLine($"You have {game.Lives} lives remaining. Insert your guess: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int guess))
            {
                if (guess == targetNumber)
                {
                    Console.WriteLine("Congratulations! You guessed the number!");
                    game.Result = true;
                    _service.StopGame(game);
                    return;
                }
                else if (guess < targetNumber)
                {
                    Console.WriteLine("Too low! Try again.");
                }
                else
                {
                    Console.WriteLine("Too high! Try again.");
                }

                game.Lives--;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        Console.WriteLine("You are out of lives! You lost the game.");
        game.Result = false;
        _service.StopGame(game);
    }


}
