namespace ExpenseTracker.Domain;

public interface IExpenseRepository
{
    public List<Expense> LoadExpenses();
    public void SaveExpenses(List<Expense> expenses);
    public List<Month> LoadBudgets();
    public void SaveBudgets(List<Month> months);
    public void ExportExpensesToCsv();
}
