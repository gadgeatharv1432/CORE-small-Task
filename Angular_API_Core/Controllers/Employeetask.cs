using DataTask.Entity;
using DataTask.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService.Services.Interfaces;

namespace Angular_API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            return Ok(ApiResponse<IEnumerable<ModelTask>>.SuccessResponse(tasks));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetTaskByIdAsync(Guid id)
        {
            var task = await taskService.GetTaskByIdAsync(id);
            if (task == null) 
                return NotFound(ApiResponse<ModelTask>.FailureResponse("Task not found.", 404));

            return Ok(ApiResponse<ModelTask>.SuccessResponse(task));
        }
        [HttpGet("{Completed}")]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetCompletedTasks()
        {
            var task = await taskService.GetCompletedTaskAsync();
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ModelTask>>> CreateTask(ModelTask taskmodel)
        {
            taskmodel.Id = Guid.NewGuid();
            await taskService.CreateTaskAsync(taskmodel);
            return CreatedAtAction(nameof(GetTaskByIdAsync),
                new { id = taskmodel.Id },
                ApiResponse<ModelTask>.SuccessResponse(taskmodel, "Task created successfully."));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTask(Guid id)
        {
            await taskService.DeleteTaskAsync(id);
            return Ok(ApiResponse<object>.SuccessResponse(null, "Task deleted successfully."));
        }
    }
}
