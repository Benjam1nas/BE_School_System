using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;
using System.Security.Cryptography;
using System.Text;

namespace SchoolSystemBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SchoolDbContext _context;

        public UserController(SchoolDbContext context)
        {
            _context = context;
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return HashPassword(inputPassword) == storedHash;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            var user = await _context.Users
                .Include(u => u.TemporaryLogin)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized(new { error = "Invalid credentials" }); // Return 401 on failure
            }

            // Set a cookie to identify the user
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,  // Set to true in production
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(30)
            };

            HttpContext.Response.Cookies.Append("UserId", user.Id.ToString(), cookieOptions);
            HttpContext.Response.Cookies.Append("UserName", user.Username, cookieOptions);
            HttpContext.Response.Cookies.Append("UserRole", user.Role.ToString(), cookieOptions);

            return Ok(new { success = true, username = user.Username, role = user.Role.ToString(), userId = user.Id.ToString()}); // Return success
        }

        [HttpPost("temporary-login")]
        public async Task<IActionResult> TemporaryLogin([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            var tempLogin = await _context.TemporaryLogins
                .Include(tl => tl.User)
                .FirstOrDefaultAsync(tl => tl.Username == request.Username && tl.Status);

            if (tempLogin == null || !VerifyPassword(request.Password, tempLogin.PasswordHash))
            {
                return Unauthorized(new { error = "Invalid temporary credentials" });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(30)
            };

            HttpContext.Response.Cookies.Append("UserId", tempLogin.UserId.ToString(), cookieOptions);
            HttpContext.Response.Cookies.Append("UserName", tempLogin.Username, cookieOptions);
            Console.WriteLine($"Temporary Login found: {tempLogin.Id}");

            // Return temporaryLoginId in the response without disabling the status
            return Ok(new { success = true, temporaryLoginId = tempLogin.Id, username = tempLogin.Username });
        }



        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserName");

            return Ok(new { success = true }); // Return success
        }

        [HttpGet]
        public IActionResult IsLoggedIn()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                return Json(new { isLoggedIn = true });
            }

            return Json(new { isLoggedIn = false });
        }

        [HttpPost("hash-password")]
        public IActionResult HashPasswordForTesting([FromBody] string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                return BadRequest(new { error = "Password is required." });
            }

            string hashedPassword = HashPassword(plainPassword);
            return Ok(new { plainPassword, hashedPassword });
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUserCredentials([FromBody] UpdateUserRequest newCredentials)
        {
            if (newCredentials == null || string.IsNullOrEmpty(newCredentials.Username) || string.IsNullOrEmpty(newCredentials.Password))
            {
                return BadRequest(new { error = "New username and password are required." });
            }

            // Check if the username is already taken
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == newCredentials.Username);
            if (existingUser != null)
            {
                return BadRequest(new { error = "Username is already taken. Please choose a different username." });
            }

            // Find the user associated with the temporaryLoginId
            var tempLogin = await _context.TemporaryLogins
                .Include(tl => tl.User)
                .FirstOrDefaultAsync(tl => tl.Id == newCredentials.TemporaryLoginId && tl.Status);

            if (tempLogin == null)
            {
                return Unauthorized(new { error = "Invalid temporary login session." });
            }

            // Update the user associated with the temporary login
            var user = tempLogin.User;
            user.Username = newCredentials.Username; // Update username in Users table
            user.Password = HashPassword(newCredentials.Password); // Update hashed password in Users table

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Disable the temporary login
            tempLogin.Status = false; // Mark temporary login as used
            _context.TemporaryLogins.Update(tempLogin);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "User credentials updated successfully." });
        }

        public class UpdateUserRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public int TemporaryLoginId { get; set; }  // Add temporaryLoginId here
        }



        [HttpPost("checkTemporaryLogin")]
        public async Task<IActionResult> CheckTemporaryLogin([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            // Check for temporary login
            var tempLogin = await _context.TemporaryLogins
                .FirstOrDefaultAsync(t => t.Username == request.Username && t.PasswordHash == request.Password);

            if (tempLogin == null)
            {
                return Ok(new { success = false }); // Temporary login failed
            }

            // If temporary login is successful, return the temporary login ID
            return Ok(new { success = true, temporaryLoginId = tempLogin.Id });
        }

    }
}
