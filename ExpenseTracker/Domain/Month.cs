namespace ExpenseTracker.Domain;

public class Month(string name)
{
    public string Name { get; set; } = name;
    public int TotalExpenses { get; set; } = 0;
    public int? Budget { get; set; } = null;
    public bool IsCritical { get; set; } = false;

    public void UpdateExpenses(int expense)
    {
        if (expense <= 0)
            return;
        TotalExpenses += expense;
    }

    public void SetBudget(int? budget)
    {
        if(budget <= 0 || budget == null)
            Budget = 0;
        else
            Budget = budget;
    }
}
