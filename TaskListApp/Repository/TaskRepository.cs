using Dapper;
using TaskListApp.DataContext;
using TaskListApp.Models;
using TaskListApp.Repository.Interfaces;

namespace TaskListApp.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDapperContext _dapperContext;

        public TaskRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public TaskItem AddTask(TaskItem task)
        {
           using(var connection = _dapperContext.CreateConnection()) 
           {
                var sql = @"INSERT INTO [TaskItem]([Title], [Deadline], [Status], [Urgency]) VALUES (@Title, @Deadline, @Status, @Urgency)";

                connection.Execute(sql, new
                {
                    task.Title,
                    task.Deadline,
                    task.Status,
                    task.Urgency
                });

                return task;
           }
        }

        public TaskItem? FindTaskById(int id)
        {
            using(var connection = _dapperContext.CreateConnection()) 
            {
                var sql = @"SELECT * FROM [TaskItem] WHERE [Id]=@id";

                return connection.QueryFirstOrDefault<TaskItem>(sql, new { id });
                

            }
        }


        public  IList<TaskItem> ListTask()
        {
            using(var connection = _dapperContext.CreateConnection()) 
            {

                var sql = @"SELECT * FROM [TaskItem]";

                return (IList<TaskItem>)connection.Query<TaskItem>(sql);
                
            }
        }

        public TaskItem Update(TaskItem task)
        {
            using(var connection = _dapperContext.CreateConnection()) 
            {
                var sql = @"UPDATE [TaskItem] SET Title=@Title, Deadline=@Deadline, Status=@Status, Urgency=@Urgency WHERE [Id]=@Id";

                connection.Execute(sql, new
                {
                    task.Title,
                    task.Deadline,
                    task.Status,
                    task.Urgency,
                    task.Id,

                });
                return task;
            }
        }
    }
}
