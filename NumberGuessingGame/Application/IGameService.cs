using NumberGuessingGame.Domain;

namespace NumberGuessingGame.Application;

public interface IGameService
{
    public Game StartGame();
    public void StopGame(Game game);
    public void ShowAllGames();
    public void ShowAllGamesWon();
    public void ShowAllGamesLost();
    public void ShowGameById(int id);
    public void DeleteAll();
}
