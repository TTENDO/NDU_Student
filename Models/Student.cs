using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NDU_Student.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        #region Navigation Properties
        public virtual Course Course { get; set; }
        #endregion
    }
}
