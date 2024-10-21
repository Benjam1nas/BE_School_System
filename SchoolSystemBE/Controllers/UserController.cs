using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;


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


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
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

            return Ok(new { success = true, username = user.Username }); // Return success
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


    }

}