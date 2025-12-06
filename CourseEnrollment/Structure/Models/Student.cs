namespace CourseEnrollment.Structure.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Simple, no hashing for this assessment
        public List<CourseEnrollments> Enrollments { get; set; } = new();
    }
}
