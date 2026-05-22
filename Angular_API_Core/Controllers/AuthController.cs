using DataTask.DTO;
using DataTask.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService.Services.Interfaces;

namespace Angular_API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _authService.LoginAsync(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid Email or Password"
                });
            }
            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.Role
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(ModelUser model)
        {
            var user = new ModelUser
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };
            await _authService.RegisterAsync(user);
            return Ok(new
            {
                message = "User Registered Successfully"
            });
        }
    }
}
