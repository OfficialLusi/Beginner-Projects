using Newtonsoft.Json;

namespace TaskTracker.Domain;

public interface ITaskRepository
{
    public List<TaskProp> LoadTasks();
    public void SaveTasks(List<TaskProp> tasks);
}
