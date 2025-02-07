using System.CommandLine;
using NumberGuessingGame.obsolete.Domain;
using NumberGuessingGame.obsolete.Infrastructure;

namespace NumberGuessingGame.obsolete.Application
{
    public class NumberGuessingGameCli
    {
        private readonly IGuessingGameRepository _repository;
        private readonly GuessingGameManager _manager;
        private readonly IGuessingGameService _service;
        private readonly string _filePath = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\NumberGuessingGame\\games.json";

        public NumberGuessingGameCli()
        {
            _repository = new GuessingGameRepository(_filePath);
            _manager = new GuessingGameManager(_repository);
            _service = new GuessingGameService(_manager);
        }

        public async Task NumberGuessingGameCliMain(string[] args)
        {
            var rootCommand = BuildRootCommand();

            // Ciclo principale per mantenere il programma in esecuzione
            while (true)
            {
                Console.WriteLine("\nWelcome to the Number Guessing Game!");
                Console.WriteLine("Type 'help' to see available commands or 'exit' to quit.\n");

                Console.Write("> "); // Prompt dell'utente
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Invalid input. Please enter a command.");
                    continue;
                }

                if (userInput.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("Thank you for playing! Goodbye!");
                    break;
                }

                try
                {
                    // Esegui il comando inserito dall'utente
                    var inputArgs = userInput.Split(' ');
                    await rootCommand.InvokeAsync(inputArgs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing your command: {ex.Message}");
                }
            }
        }


        private RootCommand BuildRootCommand()
        {
            // define "start" command
            var startCommand = new Command("start-game", "Start a new game");
            startCommand.SetHandler(StartGame);

            // define "restore" command
            var restoreCommand = new Command("restore-game", "Restore a saved game");
            restoreCommand.SetHandler(_service.RestoreGame);

            // define "save" command
            Option<int> saveGameId = new Option<int>("--id", "id for saving the game") { IsRequired = true };
            var saveCommand = new Command("save", "Save the current game") { saveGameId };
            saveCommand.SetHandler(_service.SaveGame, saveGameId);

            // define "stop" command
            Option<int> stopGameId = new Option<int>("--id", "id for stopping the game") { IsRequired = true };
            var stopCommand = new Command("stop", "Stop the current game") { stopGameId };
            stopCommand.SetHandler(_service.StopGame, stopGameId);

            var showAllGamesCommand = new Command("show-all", "Show all games");
            showAllGamesCommand.SetHandler(_service.ShowAllGames);


            var rootCommand = new RootCommand
            {
                startCommand,
                restoreCommand,
                saveCommand,
                stopCommand,
                showAllGamesCommand
            };

            rootCommand.Description = "Number Guessing Game CLI - Manage and play your guessing games.";

            return rootCommand;
        }

        #region Helper Methods

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
                        _service.StopGame(game.Id);
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
            _service.StopGame(game.Id);
        }
        private void StartGame()
        {
            var game = _service.StartGame();
            Console.WriteLine($"Game started! Game ID: {game.Id}, Lives: {game.Lives}");
            PlayGame(game);
        }

        #endregion
    }
}
