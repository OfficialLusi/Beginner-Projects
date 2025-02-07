namespace ExpenseTracker.Application;

public interface IExpenseService
{
    public void Add(string description, int amount, string category);
    public void Update(int id, string description, int amount, string category);
    public void Delete(int id);
    public void ListAllExpenses();
    public void ViewMoneySpent();
    public void ListExpensesPerCategory(string category);   
    public void ListExpensesPerMonth(int month);
    public void DeleteAll();
    public void SetBudgetForMonth(int month, int budget);
    public void IsMonthCritical(int month);
    public void CreateCsv();
}
