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

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequestDto request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _authService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequestDto request)
        {
            var updatedUser = await _authService.updatedUserAsync(id, request);
            if (updatedUser == null)
                return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authService.DeleteUserAsync(id);
            if (!success)
                return NotFound();
            return NoContent(); // 204 응답
        }
    }
}
