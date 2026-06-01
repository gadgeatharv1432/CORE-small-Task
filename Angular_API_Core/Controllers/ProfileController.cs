using DataTask.Common;
using DataTask.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskService.Services.Interfaces;

namespace Angular_API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IAuthService _authService;

        public ProfileController(IAuthService authService)
        {
            _authService = authService;
        }

        private Guid GetCurrentUserId()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(idClaim!);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetCurrentUserId();
            var profile = await _authService.GetProfileAsync(userId);

            if (profile == null) return NotFound(ApiResponse<object>.FailureResponse("User not found.", 404));

            return Ok(ApiResponse<GetProfileDTO>.SuccessResponse(
                    profile, "Profile retrieved successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.FailureResponse("Invalid data."));

            var userId = GetCurrentUserId();
            var success = await _authService.UpdateProfileAsync(userId, dto);

            if (!success) return NotFound(ApiResponse<object>.FailureResponse("User not found.", 404));

            return Ok(ApiResponse<object>.SuccessResponse(null, "Profile updated successfully."));
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.FailureResponse("Invalid data."));

            var userId = GetCurrentUserId();
            var success = await _authService.ChangePasswordAsync(userId, dto);

            if (!success) return BadRequest(
                    ApiResponse<object>.FailureResponse("Current password is incorrect.", 400));

            return Ok(
                ApiResponse<object>.SuccessResponse(null, "Password changed successfully."));
        }
    }
}
