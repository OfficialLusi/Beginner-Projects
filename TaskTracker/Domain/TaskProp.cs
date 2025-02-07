namespace TaskTracker.Domain;

public class TaskProp(int id, string description)
{

    public int Id { get; set; } = id;
    public string Description { get; set; } = description;
    public string Status { get; set; } = "todo";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.Now;
    }

}
