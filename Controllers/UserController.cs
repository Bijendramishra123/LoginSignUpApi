using Microsoft.AspNetCore.Mvc;
using Ramayan_gita_app.Models; // User & LoginRequest models
using Ramayan_gita_app.DataAccess; // Data access layer

namespace Ramayan_gita_app.Controllers
{
    [ApiController]
    [Route("api/auth")] // Base route: http://localhost:5297/api/auth
    public class AuthController : ControllerBase
    {
        private readonly UserDataAccess _userDataAccess;

        public AuthController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        // ✅ POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { message = "Invalid user data." });

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash))
                return BadRequest(new { message = "Email and Password are required." });

            try
            {
                var success = _userDataAccess.Register(user);
                if (success)
                {
                    return Ok(new { message = "User registered successfully!" });
                }

                return Conflict(new { message = "Registration failed. Email may already be in use." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        // ✅ POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Invalid login request." });

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.PasswordHash))
                return BadRequest(new { message = "Email and Password are required." });

            try
            {
                var loggedInUser = _userDataAccess.Login(request.Email, request.PasswordHash);
                if (loggedInUser != null)
                {
                    return Ok(new
                    {
                        message = "Login successful!",
                        user = loggedInUser
                    });
                }

                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }
    }
}
