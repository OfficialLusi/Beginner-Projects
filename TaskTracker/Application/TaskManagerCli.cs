using TaskTracker.Domain;
using TaskTracker.Infrastructure;

namespace TaskTracker.Application;

public class TaskManagerCli
{
    private readonly TaskService _taskService;
    private readonly TaskManager _taskManager;
    private readonly TaskRepository _taskRepository;

    public TaskManagerCli()
    {
        _taskRepository = new TaskRepository("tasks.json");
        _taskManager = new TaskManager(_taskRepository);
        _taskService = new TaskService(_taskManager);
    }

    public void TaskManagerCliMain()
    {

        Console.WriteLine("Welcome to Task Manager CLI!   ");
        Help();

        while (true)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Insert a valid number.");
                continue;
            }

            // Split input in base agli spazi
            string[] commandArgs = input.Split(' ', 2);
            string command = commandArgs[0].ToLower();

            switch (command)
            {
                case "add":
                    _taskService.Add(commandArgs);
                    break;

                case "update":
                    _taskService.Update(commandArgs);
                    break;

                case "delete":
                    _taskService.Delete(commandArgs);
                    break;
                
                case "mark-todo":
                    _taskService.MarkTodo(commandArgs);
                    break;

                case "mark-in-progress":
                    _taskService.MarkInProgress(commandArgs);
                    break;

                case "mark-done":
                    _taskService.MarkDone(commandArgs);
                    break;

                case "list-all":
                    _taskService.ListAll();
                    break;

                case "list-todo":
                    _taskManager.ListTasks("todo");
                    break;

                case "list-in-progress":
                    _taskManager.ListTasks("in-progress");
                    break;

                case "list-done":
                    _taskManager.ListTasks("done");
                    break;

                case "exit":
                    Console.WriteLine("Exit program.");
                    return;

                case "help":
                    Help();
                    break;

                default:
                    Console.WriteLine("Unknow command.");
                    break;
            }
        }
    }

    private void Help()
    {
        Console.WriteLine("you can use commands like:   \n" +
                          "   - add <activity>,         \n" +
                          "   - update <id> <activity>, \n" +
                          "   - delete <id>,            \n" +
                          "   - mark-todo <id>,         \n" +
                          "   - mark-in-progress <id>,  \n" +
                          "   - mark-done <id>,         \n" +
                          "   - list-all,               \n" +
                          "   - list-todo,              \n" +
                          "   - list-in-progress,       \n" +
                          "   - list-done,              \n" +
                          "   - exit                    \n" +
                          "   - help                      ");
    }

}
