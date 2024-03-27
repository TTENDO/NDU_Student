using Microsoft.EntityFrameworkCore;
using NDU_Student.Models;

namespace NDU_Student.ViewModels
{
    public class CourseUnitViewModel
    {
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public string? SearchTerm { get; set; }
        public List<CourseUnit> CourseUnitItems { get; set; } = new List<CourseUnit>();

        public void LoadCourseUnits(StudentDbContext context)
        {
            CourseUnitItems = context.CourseUnits.Include(r=>r.Course).ToList();
        }
    }
}
