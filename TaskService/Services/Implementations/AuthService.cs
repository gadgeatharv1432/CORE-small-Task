using DataTask.Common;
using DataTask.DTO;
using DataTask.Entity;
using Microsoft.Extensions.Options;
using TaskRepository.Repository.Interfaces;
using TaskService.Services.Interfaces;

namespace TaskService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDTO?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
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
                TokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes)
            };
        }

        public async Task<bool> RegisterAsync(RegisterDTO model)
        {
            var existing = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existing != null)
                return false;

            var user = new ModelUser
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }
        public async Task<GetProfileDTO?> GetProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new GetProfileDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<bool> UpdateProfileAsync(Guid userId, UpdateProfileDTO dto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.UserName = dto.UserName;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDTO dto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            if (user.Password != dto.CurrentPassword)
                return false;

            user.Password = dto.NewPassword;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}