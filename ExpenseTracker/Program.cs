using ExpenseTracker.Application;

namespace ExpenseTracker;

public class Program
{
    public static void Main(string[] args)
    {
        ExpensesManagerCli cli = new ExpensesManagerCli();
        cli.ManagerCliMain();
    }
}
