using NumberGuessingGame.obsolete.Domain;

namespace NumberGuessingGame.obsolete.Application;

public interface IGuessingGameService
{
    public Game StartGame();
    public void StopGame(int id);
    public void SaveGame(int id);
    public void RestoreGame();
    public void ShowAllGames();
}
