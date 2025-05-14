using BulletinBoard.Data.Models;
using BulletinBoard.Data;
using Microsoft.AspNetCore.Mvc;
using BulletinBoard.API.Models;

namespace BulletinBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("User already exists.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User created.");
        }
        [HttpPost("login")]

        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.UserName && u.Password == model.Password);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok("Login successful");
        }


    }
}
