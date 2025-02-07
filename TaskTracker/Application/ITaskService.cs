namespace TaskTracker.Application;

public interface ITaskService
{
    public void Add(string[] commandArgs);

    public void Update(string[] commandArgs);

    public void Delete(string[] commandArgs);

    public void MarkTodo(string[] commandArgs);

    public void MarkInProgress(string[] commandArgs);

    public void MarkDone(string[] commandArgs);

    public void ListAll();

    public void ListToDo();

    public void ListInProgress();

    public void ListDone();
}
