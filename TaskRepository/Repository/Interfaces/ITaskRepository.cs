using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskRepository.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ModelTask>> GetAllAsync();
        Task<ModelTask> GetByIdAsync(Guid id);
        Task AddAsync(ModelTask task);
        Task UpdateAsync(ModelTask task);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ModelTask>> GetCompletedTasksAsync();
    }
}
