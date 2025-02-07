using Newtonsoft.Json;
using TaskTracker.Domain;

namespace TaskTracker.Infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public TaskRepository(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
            CreateEmptyJsonFile(_filePath);
    }

    public List<TaskProp> LoadTasks()
    {
        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TaskProp>>(json) ?? new List<TaskProp>();
    }

    public void SaveTasks(List<TaskProp> tasks)
    {
        var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    private static void CreateEmptyJsonFile(string filePath)
    {
        File.WriteAllText(filePath, "[]");
    }
}
