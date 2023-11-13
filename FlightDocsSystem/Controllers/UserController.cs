using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Mvc;
namespace FlightDocsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string username, string password, string email, string phoneNumber)
        {
            try
            {
                var result = await _userService.Register(username, password, email, phoneNumber);
                if (result == null)
                {
                    return BadRequest("User registration failed.");
                }

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"User registration failed: {ex.Message}");
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var token = await _userService.Login(email, password);
                if (token == null)
                {
                    return Unauthorized("Sai email hoặc mật khẩu");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }
    }
}
