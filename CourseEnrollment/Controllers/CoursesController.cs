using CourseEnrollment.Structure.Data;
using CourseEnrollment.Structure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> GetCourses()
            => await _context.Courses.ToListAsync();

        [HttpPost("{courseId}/enroll/{studentId}")]
        public async Task<IActionResult> Enroll(int courseId, int studentId)
        {
            if (_context.Enrollments.Any(e => e.CourseId == courseId && e.StudentId == studentId))
            {
                return BadRequest("Already enrolled.");
            }

            _context.Enrollments.Add(new CourseEnrollments { CourseId = courseId, StudentId = studentId });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{courseId}/unenroll/{studentId}")]
        public async Task<IActionResult> Unenroll(int courseId, int studentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(studentId, courseId);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("student/{studentId}")]
        public async Task<IEnumerable<Course>> GetStudentCourses(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course)
                .ToListAsync();
        }
    }

}
