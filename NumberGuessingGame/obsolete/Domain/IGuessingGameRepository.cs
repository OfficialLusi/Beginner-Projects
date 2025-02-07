namespace NumberGuessingGame.obsolete.Domain;

public interface IGuessingGameRepository
{
    public List<Game> LoadGames();
    public void SaveGames(List<Game> games);
}
