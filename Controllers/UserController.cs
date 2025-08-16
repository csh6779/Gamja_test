using GamjaTest.Dtos;
using GamjaTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamjaTest.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;

        public UsersController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequestDto request)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(request);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
