using NDU_Student.Models;

namespace NDU_Student.ViewModels
{
    public class CourseViewModel
    {
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public string? SearchTerm { get; set; }
        public List<Course> CourseItems { get; set; } = new List<Course>();

        public void LoadCourses(StudentDbContext context)
        {
            CourseItems = context.Courses.ToList();
        }
    }
}
