using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ModelTask> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<ModelTask>> GetAllTasksAsyncNewUpload();
        Task<IEnumerable<ModelTask>> GetCompletedTaskAsync();
        Task CreateTaskAsync(ModelTask taskmodel);
        Task UpdateTaskAsync(ModelTask taskmodel);
        Task DeleteTaskAsync(Guid id);
    }
}
