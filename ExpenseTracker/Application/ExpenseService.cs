using ExpenseTracker.Domain;
using System.Globalization;

namespace ExpenseTracker.Application;

public class ExpenseService(ExpenseManager manager) : IExpenseService
{
    private readonly ExpenseManager _manager = manager;

    /*
     * The input should be something like:
     * add --description "description" --amount amount --category "category"
     */
    public void Add(string description, int amount, string category)
    {
        try
        {
            _manager.AddExpense(description, amount, category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /*
     * The input should be something like:
     * update --id 3 --newdescription "description" --newamount "amount"
     */
    public void Update(int id, string description, int amount, string category)
    {
        try
        {
            _manager.UpdateExpense(id, description, amount, category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /*
    * The input should be something like:
    * delete --id 3 
    */
    public void Delete(int id)
    {
        try
        {
            _manager.DeleteExpense(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /*
    * The input should be something like:
    * list-all-expenses 
    */
    public void ListAllExpenses()
    {
        try
        {
            _manager.ListAllExpenses();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void ListExpensesPerCategory(string category)
    {
        try
        {
            _manager.SelectExpensePerCategory(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }


    /*
    * The input should be something like:
    * view-expenses-per-month --month "month-name" or "2"
    */
    public void ListExpensesPerMonth(int month)
    {
        DateTimeFormatInfo dtfi = CultureInfo.InvariantCulture.DateTimeFormat;
        try
        {
            _manager.ListExpensesPerMonth(month);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /*
    * The input should be something like:
    * view-all-money-spent
    */
    public void ViewMoneySpent()
    {
        try
        {
            _manager.ViewAllMoneySpent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void DeleteAll()
    {
        try
        {
            _manager.DeleteAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void SetBudgetForMonth(int month, int budget)
    {
        try
        {
            _manager.SetBudgetForMonth(month, budget);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void IsMonthCritical(int month)
    {
        try
        {
            _manager.IsMonthCritical(month);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void CreateCsv()
    {
        try
        {
            _manager.CreateCsv();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
