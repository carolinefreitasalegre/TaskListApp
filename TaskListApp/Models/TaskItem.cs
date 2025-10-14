using System.ComponentModel.DataAnnotations.Schema;
using TaskListApp.Enum;

namespace TaskListApp.Models
{
    [Table("[TaskItem]")]
    public class TaskItem
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public EStatus Status { get; set; }
        public EUrgency Urgency { get; set; }
    }
}
