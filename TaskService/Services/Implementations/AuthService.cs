using DataTask.DTO;
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
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<AuthResponseDTO?> LoginAsync(string email, string password)
        {
            var user  = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password)
                return null;
            var token = _jwtService.GenerateToken(user);
            return new AuthResponseDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = token,
                TokenExpiry = DateTime.UtcNow.AddMinutes(60)
            };
        }
        public async Task<bool> RegisterAsync(RegisterDTO model)
        {
            // Check if email already exists
            var existing = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existing != null)
                return false;

            var user = new ModelUser
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password, // Note: hash this in production
                Role = model.Role
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }
    }
}
