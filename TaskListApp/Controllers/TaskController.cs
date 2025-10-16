using Microsoft.AspNetCore.Mvc;
using TaskListApp.DTO;
using TaskListApp.Enum;
using TaskListApp.Filters;
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

        private int GetUser()
        {
            return int.Parse(User.FindFirst("sub")?.Value ?? "0");
        }


        [AuthorizePerfil(EPerfilType.Usuario)]
        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItemDto taskDto)
        {
            var userId = GetUser();

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Deadline = taskDto.Deadline,
                Status = taskDto.Status,
                Urgency = taskDto.Urgency,

                //mudar futuramente para pegar o login
                UserId = userId

            };
            var retorno = _repository.AddTask(task, userId);
            return Ok(task);
        }

        [AuthorizePerfil(EPerfilType.Usuario)]
        [HttpGet]
        public ActionResult<TaskItem> GetAllTasks()
        {
            var retorno = _repository.ListTask();
            return Ok(retorno);
        }

        [AuthorizePerfil(EPerfilType.Usuario)]
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var retorno = _repository.FindTaskById(id);
            return Ok(retorno);
        }

        [AuthorizePerfil(EPerfilType.Usuario)]
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
