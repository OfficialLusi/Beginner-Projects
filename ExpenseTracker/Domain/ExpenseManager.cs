using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ExpenseTracker.Domain;

public class ExpenseManager
{

    private readonly IExpenseRepository _repository;
    private readonly List<Expense> _expenses;
    private readonly List<Month> _months;

    public ExpenseManager(IExpenseRepository repo)
    {
        _repository = repo;
        _expenses = _repository.LoadExpenses();
        _months = _repository.LoadBudgets();
    }

    public void AddExpense(string description, int amount, string category)
    {
        int newId = _expenses.Count > 0 ? _expenses.Max(e => e.Id) + 1 : 1;
        Expense newExpense = new Expense(newId, amount, description, category);
        _expenses.Add(newExpense);
        _repository.SaveExpenses(_expenses);
        Console.WriteLine($"Expense added succesfully (ID: {newId})\n");

        string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(newExpense.CreatedAt.Month);

        if (_months.Any(x => x.Name == monthName))
        {
            Month month = _months.FirstOrDefault(x => x.Name == monthName);
            month.TotalExpenses += newExpense.Amount;
            month.IsCritical = month.TotalExpenses > month.Budget;
            _repository.SaveBudgets(_months);
        }
    }

    public void UpdateExpense(int id, string description, int amount, string category)
    {
        Expense expense = _expenses.FirstOrDefault(e => e.Id == id);

        if (expense != null)
        {
            string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(expense.CreatedAt.Month);

            bool monthBool = _months.Any(x => x.Name == monthName);

            if (monthBool)
            {
                Month month = _months.FirstOrDefault(x => x.Name == monthName);
                month.TotalExpenses -= expense.Amount;
                month.TotalExpenses += amount;
                month.IsCritical = month.TotalExpenses > month.Budget;
                _repository.SaveBudgets(_months);
            }

            expense.Amount = amount;
            expense.Description = description;
            expense.Category = category;
            expense.UpdateTime();

            _repository.SaveExpenses(_expenses);
            Console.WriteLine($"Expense updated successfully (ID: {id})\n");
        }
        else
        {
            Console.WriteLine($"Expense with ID: {id} not found\n");
        }
    }

    public void DeleteExpense(int id)
    {
        Expense expense = _expenses.FirstOrDefault(e => e.Id == id);
        if (expense != null)
        {
            string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(expense.CreatedAt.Month);

            if (_months.Any(x => x.Name == monthName))
            {
                Month month = _months.FirstOrDefault(x => x.Name == monthName);
                month.TotalExpenses -= expense.Amount;
                month.IsCritical = !(month.TotalExpenses < month.Budget);
                _repository.SaveBudgets(_months);
            }

            _expenses.Remove(expense);
            _repository.SaveExpenses(_expenses);
            Console.WriteLine($"Expense deleted succesfully (ID: {id})\n");
        }
        else Console.WriteLine($"Expense with ID: {id} not found\n");
    }

    public void ListAllExpenses()
    {
        if (_expenses.Count == 0)
        {
            Console.WriteLine("No expenses found.");
        }
        else
        {
            foreach (Expense expense in _expenses)
            {
                Console.WriteLine($"Expense Id: {expense.Id}");
                Console.WriteLine($"Expense Description: {expense.Description}");
                Console.WriteLine($"Expense Amount: {expense.Amount}");
                Console.WriteLine($"Expense Category: {expense.Category}");
                Console.WriteLine($"Expense Created At: {expense.CreatedAt}");

                if (expense.UpdatedAt != null)
                {
                    Console.WriteLine($"Expense Updated At: {expense.UpdatedAt}");
                }

                Console.WriteLine();
            }
        }
    }

    public void ViewAllMoneySpent()
    {
        int totalAmount = 0;
        foreach (Expense expense in _expenses)
            totalAmount += expense.Amount;

        Console.WriteLine($"Total Amount Spent : {totalAmount}\n");
    }

    public void SelectExpensePerCategory(string category)
    {
        if (_expenses.Any(e => e.Category == category))
        {
            int cateogoryTotalAmount = 0;
            Console.WriteLine($"For category: {category} there were these expenses:");
            foreach (Expense expense in _expenses)
            {
                if (expense.Category == category)
                {
                    Console.WriteLine($"On date: {expense.CreatedAt} the expense was: {expense.Description} for an amount of: {expense.Amount}");
                    cateogoryTotalAmount += expense.Amount;
                }
            }
            Console.WriteLine($"For a total amount of the category: {category} of: {cateogoryTotalAmount}");
            return;
        }
        Console.WriteLine($"There were no expenses for category: {category}");
    }

    public void ListExpensesPerMonth(int month)
    {
        string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);
        if (_expenses.Any(x => x.CreatedAt.Month == month))
        {
            int monthTotalAmount = 0;
            Console.WriteLine($"In {monthName} you had these expenses:");
            foreach (Expense expense in _expenses)
            {
                if (expense.CreatedAt.Month == month)
                {
                    Console.WriteLine($"On date: {expense.CreatedAt} the expense was: {expense.Description} for an amount of: {expense.Amount} in the category: {expense.Category}");
                    monthTotalAmount += expense.Amount;
                }
            }
            Console.WriteLine($"For a total amount of: {monthTotalAmount}");
            return;
        }
        Console.WriteLine($"There were no expenses in {monthName}");
    }

    public void DeleteAll()
    {
        _expenses.Clear();
        _repository.SaveExpenses(_expenses);
        Console.WriteLine("All expenses has been eliminated correctly");
    }

    public void SetBudgetForMonth(int month, int budget)
    {
        string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);

        if (_months.Any(x => x.Name == monthName))
        {
            Month existingMonth = _months.FirstOrDefault(x => x.Name == monthName);

            existingMonth.Budget = budget;

            existingMonth.IsCritical = existingMonth.TotalExpenses > existingMonth.Budget;

            Console.WriteLine($"Budget updated correctly for month: {monthName}, budget: {budget}");
        }
        else
        {
            Month monthToAdd = new Month(monthName);

            monthToAdd.TotalExpenses = _expenses
                .Where(x => x.CreatedAt.Month == month)
                .Sum(x => x.Amount);

            monthToAdd.Budget = budget;
            monthToAdd.IsCritical = monthToAdd.TotalExpenses > monthToAdd.Budget;

            _months.Add(monthToAdd);

            Console.WriteLine($"Budget created correctly for month: {monthName}, budget: {budget}");
        }

        _repository.SaveBudgets(_months);
    }

    public void IsMonthCritical(int month)
    {
        string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);

        if (!_months.Any(x => x.Name == monthName))
        {
            Console.WriteLine($"You did not inserted a budget for {monthName}");
            return;
        }

        Month monthToCheck = _months.FirstOrDefault(x => x.Name == monthName);


        if (monthToCheck.IsCritical)
        {
            Console.WriteLine($"In {monthName} your expenses are BIGGER than the budget you inserted\nBudget: {monthToCheck.Budget}\nExpenses: {monthToCheck.TotalExpenses}");
            return;
        }
        Console.WriteLine($"In {monthName} your expenses are SMALLER than the budget you inserted\nBudget: {monthToCheck.Budget}\nExpenses: {monthToCheck.TotalExpenses}");
    }

    public void CreateCsv()
    {
        _repository.ExportExpensesToCsv();
    }
}
