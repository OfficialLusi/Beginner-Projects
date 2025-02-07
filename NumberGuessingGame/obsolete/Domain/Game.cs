namespace NumberGuessingGame.obsolete.Domain;

public class Game
{
    public int Id { get; set; }
    public string GameMode { get; set; } = string.Empty;
    public int Lives { get; set; }
    public bool Result { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public DateTime? SavedAt { get; set; } = null; // other project
    public DateTime? RestoredAt { get; set; } = null; // other project
    public TimeSpan? GameLastedFor { get; set; } = null;
    public int Attempts { get; set; }
}
