using ExpenseTracker.Domain;
using ExpenseTracker.Infrastructure;
using System.CommandLine;

namespace ExpenseTracker.Application;

public class ExpensesManagerCli
{
    private readonly ExpenseRepository _repository;
    private readonly ExpenseManager _manager;
    private readonly ExpenseService _service;
    private const string _filePathForExpenses = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\ExpenseTracker\\expenses.json";
    private const string _filePathForBudgets = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\ExpenseTracker\\budjets.json";
    private const string _csvFilePath = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\ExpenseTracker\\expenses.csv";

    public ExpensesManagerCli()
    {
        _repository = new ExpenseRepository(_filePathForExpenses, _filePathForBudgets, _csvFilePath);
        _manager = new ExpenseManager(_repository);
        _service = new ExpenseService(_manager);
    }

    public void ManagerCliMain()
    {
        var rootCommand = new RootCommand("A simple CLI Expense Tracker application");

        // add command
        Option<string> addDescriptionOption = new Option<string>("--description", "Description of the expense") { IsRequired = true };
        Option<int> addAmountOption = new Option<int>("--amount", "Amount of the expense") { IsRequired = true };
        Option<string> addCategoryOption = new Option<string>("--category", "Category of the expense") { IsRequired = true };

        var addCommand = new Command("add", "Adds a new expense -> add --description \"desc\" --amount amount --category \"category\"")
            {
                addDescriptionOption,
                addAmountOption,
                addCategoryOption
            };

        addCommand.SetHandler(_service.Add,
        addDescriptionOption, addAmountOption, addCategoryOption);

        // update command
        Option<int> updateIdOption = new Option<int>("--id", "ID of the expense to update") { IsRequired = true };
        Option<string> updateDescriptionOption = new Option<string>("--newdesc", "New description for the expense") { IsRequired = true };
        Option<int> updateAmountOption = new Option<int>("--newamount", "New amount for the expense") { IsRequired = true };
        Option<string> updateCategoryOption = new Option<string>("--newcat", "New category for the expense") { IsRequired = true };


        var updateCommand = new Command("update", "Updates an existing expense -> update --id id --newdesc \"newdesc\" --newamount newamount --newcat \"newcat\"")
            {
                updateIdOption,
                updateDescriptionOption,
                updateAmountOption,
                updateCategoryOption
            };

        updateCommand.SetHandler(_service.Update,
        updateIdOption, updateDescriptionOption, updateAmountOption, updateCategoryOption);

        // delete command
        Option<int> deleteIdOption = new Option<int>("--id", "ID of the expense to delete") { IsRequired = true };

        var deleteCommand = new Command("delete", "Deletes an expense -> delete --id id")
            {
                deleteIdOption
            };

        deleteCommand.SetHandler(_service.Delete,
        deleteIdOption);

        var listCommand = new Command("list-all", "Lists all expenses -> list-all");
        listCommand.SetHandler(_service.ListAllExpenses);

        // list expenses per month command
        Option<int> monthOption = new Option<int>("--month", "List all expenses per month") { IsRequired = true };

        var listPerMonthCommand = new Command("list-month", "List all expenses of a month -> list-month --month month")
            {
                monthOption
            };

        listPerMonthCommand.SetHandler(_service.ListExpensesPerMonth,
        monthOption);

        // list expenses per category command
        Option<string> categoryOption = new Option<string>("--category", "List all expenses per category") { IsRequired = true };

        var listPerCategoryCommand = new Command("list-category", "List all expenses per category -> list-category --category \"category\"")
        {
            categoryOption
        };

        listPerCategoryCommand.SetHandler(_service.ListExpensesPerCategory,
        categoryOption);

        // view total amount spent command
        var viewTotalAmountSpentCommand = new Command("view-all", "View total amount spent -> view-all");

        viewTotalAmountSpentCommand.SetHandler(_service.ViewMoneySpent);

        // set budget for month command
        Option<int> setBudgetMonth = new Option<int>("--month", "Month you want to set the budget for") { IsRequired = true };
        Option<int> setBudgetBud = new Option<int>("--budget", "Budject you want to set the month") { IsRequired = true };

        var setBudgetCommand = new Command("set-budget", "Set a specified budget for a specified month -> set-budget --month month --budget budget")
        {
            setBudgetMonth,
            setBudgetBud
        };

        setBudgetCommand.SetHandler(_service.SetBudgetForMonth,
        setBudgetMonth, setBudgetBud);

        // is critical month command
        Option<int> isCriticalMonth = new Option<int>("--month", "Month you want to know if critical") { IsRequired = true };

        var isCriticalCommand = new Command("is-critical", "Verify if month is critical -> is-critical --month month")
        {
            isCriticalMonth
        };

        isCriticalCommand.SetHandler(_service.IsMonthCritical,
        isCriticalMonth);

        // create csv command
        var createCsvCommand = new Command("create-csv", "Create csv sheet -> create-csv");

        createCsvCommand.SetHandler(_service.CreateCsv);

        // delete all expenses command
        var deleteAllCommand = new Command("delete-all", "Delete all expenses -> delete-all");

        deleteAllCommand.SetHandler(_service.DeleteAll);

        // exit command
        var exitCommand = new Command("exit", "Exit the application -> exit");

        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(updateCommand);
        rootCommand.AddCommand(deleteCommand);
        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(listPerMonthCommand);
        rootCommand.AddCommand(listPerCategoryCommand);
        rootCommand.AddCommand(viewTotalAmountSpentCommand);
        rootCommand.AddCommand(setBudgetCommand);
        rootCommand.AddCommand(isCriticalCommand);
        rootCommand.AddCommand(createCsvCommand);
        rootCommand.AddCommand(deleteAllCommand);
        rootCommand.AddCommand(exitCommand);

        rootCommand.InvokeAsync("--help").Wait();

        while (true)
        {
            Console.Write("\nEnter a command: ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please enter a valid command.");
                continue;
            }

            string[] userArgs = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (userArgs[0].ToLower() == "exit")
            {
                Console.WriteLine("Exiting the application. Goodbye!");
                break;
            }

            rootCommand.InvokeAsync(userArgs).Wait();
        }
    }
}
