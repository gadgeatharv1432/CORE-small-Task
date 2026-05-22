using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ModelUser> LoginAsync(string email, string password);
        Task RegisterAsync(ModelUser model);
    }
}
