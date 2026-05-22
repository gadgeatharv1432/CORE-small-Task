using DataTask.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService.Services.Interfaces;

namespace Angular_API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Employeetask : ControllerBase
    {
        private readonly ITaskService taskService;

        public Employeetask(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ModelTask>>> GetTasksAsync()
        {
            var tasks = await taskService.GetAllTasksAsyncNewUpload();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetTaskByIdAsync(Guid id)
        {
            var task = await taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }
        [HttpGet("{Completed}")]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetCompletedTasks()
        {
            var task = await taskService.GetCompletedTaskAsync();
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ModelTask>>> CreateTask(ModelTask taskmodel)
        {
            await taskService.CreateTaskAsync(taskmodel);
            return CreatedAtAction(nameof(GetTaskByIdAsync), new { id = taskmodel.Id }, taskmodel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ModelTask>>> DeleteTask(Guid id)
        {
            await taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
