using DataTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTask.DTO;

namespace TaskService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(RegisterDTO model);
        Task<GetProfileDTO?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, UpdateProfileDTO dto);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDTO dto);
    }
}
