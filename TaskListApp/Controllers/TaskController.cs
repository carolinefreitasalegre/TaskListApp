using Microsoft.AspNetCore.Mvc;
using TaskListApp.Models;
using TaskListApp.Repository.Interfaces;

namespace TaskListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem task)
        {
            var retorno = _repository.AddTask(task);
            return Ok(task);
        }

        [HttpGet]
        public ActionResult<TaskItem> GetAllTasks()
        {
            var retorno = _repository.ListTask();
            return Ok(retorno);
        }
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var retorno = _repository.FindTaskById(id);
            return Ok(retorno);
        }

        [HttpPost("{id}")]
        public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
        {
            task.Id = id;
            if (id != task.Id)
                return BadRequest("Tarefa não encontrada.");

            var retorno = _repository.Update(task);
            return Ok(retorno);
        }

    }
}
