using TaskListApp.Models;

namespace TaskListApp.Repository.Interfaces
{
    public interface ITaskRepository
    {
        TaskItem AddTask(TaskItem task, int userId);
        IList<TaskItem> ListTask();
        TaskItem? FindTaskById(int id);
        TaskItem Update(TaskItem task);
        
    }
}
