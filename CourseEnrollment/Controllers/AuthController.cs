using CourseEnrollment.Structure.Data;
using CourseEnrollment.Structure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Student student)
        {
            if (_context.Students.Any(s => s.Username == student.Username))
            {
                return BadRequest("User already exists.");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPost("login")]
        public IActionResult Login(Student student)
        {
            var existing = _context.Students.FirstOrDefault(s =>
                s.Username == student.Username && s.Password == student.Password);

            if (existing == null)
            {
                return Unauthorized();
            }

            return Ok(existing);
        }
    }

}
