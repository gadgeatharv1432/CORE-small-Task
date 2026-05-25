using DataTask.DTO;
using DataTask.Entity;
using DataTask.Common;
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
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailureResponse("Invalid request data."));

            var result = await _authService.LoginAsync(model.Email, model.Password);

            if (result == null)
                return Unauthorized(ApiResponse<object>.FailureResponse("Invalid email or password.", 401));

            return Ok(ApiResponse<AuthResponseDTO>.SuccessResponse(result, "Login successful."));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailureResponse("Invalid request data."));

            var success = await _authService.RegisterAsync(model);

            if (!success)
                return Conflict(ApiResponse<object>.FailureResponse("Email already registered.", 409));

            return Ok(ApiResponse<object>.SuccessResponse(null, "User registered successfully."));
        }
    }
}
