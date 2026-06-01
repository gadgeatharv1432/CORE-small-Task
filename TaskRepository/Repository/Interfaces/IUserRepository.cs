using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskRepository.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<ModelUser>GetUserByEmailAsync(string email);
        Task AddUserAsync(ModelUser user);
        Task<ModelUser?> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(ModelUser user);

    }
}
