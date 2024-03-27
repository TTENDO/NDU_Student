using Microsoft.EntityFrameworkCore;

namespace NDU_Student.Models
{
    public class StudentDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public StudentDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseUnit> CourseUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
