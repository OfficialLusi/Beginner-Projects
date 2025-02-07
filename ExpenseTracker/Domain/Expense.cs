namespace ExpenseTracker.Domain;

public class Expense
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = null;
    public string Category { get; set; } = string.Empty;

    public Expense(int id, int amount, string description, string category)
    {
        Id = id;
        Amount = amount;
        Description = description;
        Category = category;
    }
    public void UpdateTime()
    {
        UpdatedAt = DateTime.Now;
    }
}
