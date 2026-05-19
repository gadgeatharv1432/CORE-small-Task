using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRepository.Repository.Interfaces;
using TaskService.Services.Interfaces;

namespace TaskService.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<ModelTask>> GetAllTasksAsyncNewUpload()
        {
            return await _taskRepository.GetAllAsync();
        }
        public async Task<ModelTask> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<ModelTask>> GetCompletedTaskAsync()
        {
            return await _taskRepository.GetCompletedTasksAsync();
        }
        public async Task CreateTaskAsync(ModelTask taskmodel)
        {
            await _taskRepository.AddAsync(taskmodel);
        }
        public async Task UpdateTaskAsync(ModelTask taskmodel)
        {
            await _taskRepository.UpdateAsync(taskmodel);
        }
        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
