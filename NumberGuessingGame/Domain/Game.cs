namespace NumberGuessingGame.Domain;

public class Game
{
    public int Id { get; set; }
    public GameMode Mode { get; set; }
    public int Lives { get; set; }
    public int Attempts { get; set; } = 0;
    public bool Result { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan GameDuration { get; set; }

    public enum GameMode
    {
        Easy,
        Medium,
        Hard
    }
}
