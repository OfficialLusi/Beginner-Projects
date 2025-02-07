namespace NumberGuessingGame.Domain;

public interface IGamesRepo
{
    public List<Game> LoadGames();
    public void SaveGames(List<Game> games);
    public void DeleteGames(List<Game> games);
}
