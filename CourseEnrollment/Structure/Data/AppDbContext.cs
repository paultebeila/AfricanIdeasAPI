using CourseEnrollment.Structure.Models;
using Microsoft.EntityFrameworkCore;

using CourseEnrollment.Structure.Models;

namespace CourseEnrollment.Structure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollments> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseEnrollments>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<CourseEnrollments>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<CourseEnrollments>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }

}
