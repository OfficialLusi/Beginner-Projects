using ExpenseTracker.Domain;
using Newtonsoft.Json;

namespace ExpenseTracker.Infrastructure;

public class ExpenseRepository : IExpenseRepository
{

    private readonly string _filePathForExpenses;
    private readonly string _filePathForBudgets;
    private readonly string _csvFilePath;

    public ExpenseRepository(string filePathForExpenses, string filePathForBudgets, string csvFilePath)
    {
        _filePathForExpenses = filePathForExpenses;

        _filePathForBudgets = filePathForBudgets;

        _csvFilePath = csvFilePath;
        
        if (!File.Exists(_filePathForExpenses))
            CreateEmptyJsonFileForExpenses();
        if (!File.Exists(_filePathForBudgets))
            CreateEmptyJsonFileForBudgets();
    }

    #region public methods
    public List<Expense> LoadExpenses()
    {
        try
        {
            string json = File.ReadAllText(_filePathForExpenses);
            return JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during reading json file: {ex.Message}");
            return new List<Expense>();
        }
    }


    public void SaveExpenses(List<Expense> expenses)
    {
        try
        {
            string json = JsonConvert.SerializeObject(expenses, Formatting.Indented);
            File.WriteAllText(_filePathForExpenses, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during saving json file: {ex.Message}");
        }
    }

    public List<Month> LoadBudgets()
    {
        try
        {
            string json = File.ReadAllText(_filePathForBudgets);
            return JsonConvert.DeserializeObject<List<Month>>(json) ?? new List<Month>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during reading json file: {ex.Message}");
            return new List<Month>();
        }
    }

    public void SaveBudgets(List<Month> months)
    {
        try
        {
            string json = JsonConvert.SerializeObject(months, Formatting.Indented);
            File.WriteAllText(_filePathForBudgets, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during saving json file: {ex.Message}");
        }
    }
    #endregion

    #region save in csv
    public void ExportExpensesToCsv()
    {
        try
        {
            List<Expense> expenses = LoadExpenses();

            if (expenses == null || expenses.Count == 0)
            {
                Console.WriteLine("No expenses found to export.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(_csvFilePath))
            {
                writer.WriteLine("Id,Description,Amount,Category,CreatedAt,UpdatedAt");

                foreach (var expense in expenses)
                {
                    string csvRow = $"{expense.Id},{EscapeCsvValue(expense.Description)},{expense.Amount},{EscapeCsvValue(expense.Category)},{expense.CreatedAt},{expense.UpdatedAt}";
                    writer.WriteLine(csvRow);
                }
            }

            Console.WriteLine($"Expenses exported successfully to {_csvFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while exporting expenses to CSV: {ex.Message}");
        }
    }
    #endregion

    #region private methods
    private void CreateEmptyJsonFileForExpenses()
    {
        File.WriteAllText(_filePathForExpenses, "[]");
    }
    private void CreateEmptyJsonFileForBudgets()
    {
        File.WriteAllText(_filePathForBudgets, "[]");
    }
    private string EscapeCsvValue(string value)
    {
        if (value.Contains(",") || value.Contains("\""))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
    #endregion
}
