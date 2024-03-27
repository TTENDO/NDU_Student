using Microsoft.EntityFrameworkCore;
using NDU_Student.Models;

namespace NDU_Student.ViewModels
{
    public class CourseUnitViewModel
    {
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public string? SearchTerm { get; set; }
        public int? CourseId { get; set; }
        public List<CourseUnit> CourseUnitItems { get; set; } = new List<CourseUnit>();
        public List<Course> Courses { get; set; } = new List<Course>();


        public void LoadCourseUnits(StudentDbContext context)
        {
            Courses = context.Courses.ToList();
            CourseUnitItems = context.CourseUnits.Include(r=>r.Course).ToList();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                CourseUnitItems = CourseUnitItems.Where(r => r.Name.ToLower().Contains(SearchTerm.Trim().ToLower())
                                                             || r.CourseUnitCode.ToLower()
                                                                 .Contains(SearchTerm.Trim().ToLower())
                                                             || r.DoneInYear.ToLower()
                                                                 .Contains(SearchTerm.Trim().ToLower())
                                                             || r.Course.Name.ToLower().Contains(SearchTerm.Trim().ToLower())).ToList();
            }

            if (CourseId.HasValue)
            {
                CourseUnitItems = CourseUnitItems.Where(r => r.CourseId == CourseId).ToList();
            }
        }
    }
}
