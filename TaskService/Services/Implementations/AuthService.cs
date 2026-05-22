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
    public class AuthService :IAuthService
    {
        private  readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ModelUser> LoginAsync(string email, string password)
        {
            var user  = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return null;
            if (user.Password != password)
                return null;
            return user;

        }
        public async Task RegisterAsync(ModelUser user)
        {
            await _userRepository.AddUserAsync(user);
        }
    }
}
