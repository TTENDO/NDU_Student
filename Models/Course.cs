using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDU_Student.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }

        #region Navigation Properties
        //public virtual List<Student> Students { get; set; }
        public virtual List<CourseUnit> CourseUnits { get; set; }
        #endregion
        [NotMapped]
        public string? SuccessMessage { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
}
