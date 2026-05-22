using DataTask.Context;
using DataTask.Entity;
using Microsoft.EntityFrameworkCore;
using TaskRepository.Repository.Interfaces;

namespace TaskRepository.Repository.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ModelTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }
        public async Task<ModelTask> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        public async Task AddAsync(ModelTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ModelTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<ModelTask>> GetCompletedTasksAsync()
        {
            return await _context.Tasks.Where(t => t.IsCompleted).ToListAsync();
        }
    }
}
