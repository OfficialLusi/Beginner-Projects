using System;
using TaskTracker.Application;

class Program
{
    static void Main(string[] args)
    {
        TaskManagerCli taskManagerCli = new TaskManagerCli();
        taskManagerCli.TaskManagerCliMain();
    }
}
