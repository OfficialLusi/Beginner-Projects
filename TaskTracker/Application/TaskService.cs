using TaskTracker.Domain;

namespace TaskTracker.Application;

class TaskService(TaskManager taskManager) : ITaskService
{
    private readonly TaskManager _taskManager = taskManager;

    public void Add(string[] commandArgs)
    {
        if (commandArgs.Length > 1)
        {
            string description = commandArgs[1];
            _taskManager.AddTask(description);
        }
        else
            Console.WriteLine("You need a description to add an activity");
    }

    public void Update(string[] commandArgs)
    {
        string[] updateArgs = commandArgs.Length > 1 ? commandArgs[1].Split(' ', 2) : null;
        if (updateArgs != null && updateArgs.Length == 2 && int.TryParse(updateArgs[0], out int updateId))
        {
            string newDescription = updateArgs[1];
            _taskManager.UpdateTask(updateId, newDescription);
        }
        else
            Console.WriteLine("Invalid format. Use: update <id> <nuova descrizione>");
    }

    public void Delete(string[] commandArgs)
    {
        if (commandArgs.Length > 1 && int.TryParse(commandArgs[1], out int deleteId))
            _taskManager.DeleteTask(deleteId);
        else
            Console.WriteLine("Invalid format. Use: delete <id>");
    }

    public void MarkTodo(string[] commandArgs)
    {
        if (commandArgs.Length > 1 && int.TryParse(commandArgs[1], out int toDoId))
        {
            if (_taskManager.GetTask(toDoId).Status != "todo")
            {
                _taskManager.MarkTask(toDoId, "todo");
                return;
            }
            Console.WriteLine("Task already todo.");
        }
        else
            Console.WriteLine("Invalid format. Use: mark-todo <id>");
    }

    public void MarkInProgress(string[] commandArgs)
    {
        if (commandArgs.Length > 1 && int.TryParse(commandArgs[1], out int inProgressId))
        {
            if(_taskManager.GetTask(inProgressId).Status != "in-progress")
            {
                _taskManager.MarkTask(inProgressId, "in-progress");
                return;
            }
            Console.WriteLine("Task already in progress.");
        }
        else
            Console.WriteLine("Invalid format. Use: mark-in-progress <id>");
    }

    public void MarkDone(string[] commandArgs)
    {
        if (commandArgs.Length > 1 && int.TryParse(commandArgs[1], out int doneId))
        {
            if(_taskManager.GetTask(doneId).Status != "done")
            {
                _taskManager.MarkTask(doneId, "done");
                return;
            }
            Console.WriteLine("Task already done.");

        }
        else
            Console.WriteLine("Invalid format. Use: mark-done <id>");
    }

    public void ListAll() => _taskManager.ListTasks();

    public void ListToDo() => _taskManager.ListTasks("todo");

    public void ListInProgress() => _taskManager.ListTasks("in-progress");

    public void ListDone() => _taskManager.ListTasks("done");  
}