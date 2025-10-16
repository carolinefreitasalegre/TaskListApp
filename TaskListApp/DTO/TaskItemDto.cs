using TaskListApp.Enum;
using TaskListApp.Models;

namespace TaskListApp.DTO
{
    public class TaskItemDto
    {
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public EStatus Status { get; set; }
        public EUrgency Urgency { get; set; }
        public User UserId { get; set; }
    }
}
