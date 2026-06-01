using DataTask.Entity;
using DataTask.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Services.Interfaces;

namespace Angular_API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Employeetask : ControllerBase
    {
        private readonly ITaskService _taskService;

        public Employeetask(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET /api/Employeetask
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ModelTask>>>> GetTasksAsync()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(ApiResponse<IEnumerable<ModelTask>>.SuccessResponse(tasks));
        }

        // GET /api/Employeetask/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ModelTask>>> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound(ApiResponse<ModelTask>.FailureResponse("Task not found.", 404));

            return Ok(ApiResponse<ModelTask>.SuccessResponse(task));
        }

        // GET /api/Employeetask/completed
        [HttpGet("completed")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ModelTask>>>> GetCompletedTasks()
        {
            var tasks = await _taskService.GetCompletedTaskAsync();
            return Ok(ApiResponse<IEnumerable<ModelTask>>.SuccessResponse(tasks));
        }

        // POST /api/Employeetask
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ModelTask>>> CreateTask(ModelTask taskmodel)
        {
            taskmodel.Id = Guid.NewGuid();
            await _taskService.CreateTaskAsync(taskmodel);
            return CreatedAtAction(
                nameof(GetTaskByIdAsync),
                new { id = taskmodel.Id },
                ApiResponse<ModelTask>.SuccessResponse(taskmodel, "Task created successfully."));
        }

        // DELETE /api/Employeetask/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTask(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok(ApiResponse<object>.SuccessResponse(null, "Task deleted successfully."));
        }

        // PUT /api/Employeetask/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ModelTask>>> UpdateTask(Guid id, ModelTask taskmodel)
        {
            taskmodel.Id = id;
            await _taskService.UpdateTaskAsync(taskmodel);
            return Ok(ApiResponse<ModelTask>.SuccessResponse(taskmodel, "Task updated successfully."));
        }
    }
}