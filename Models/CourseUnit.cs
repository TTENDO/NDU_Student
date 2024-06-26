﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NDU_Student.Models
{
    public class CourseUnit
    {
        public int Id { get; set; }
        public string CourseUnitCode { get; set; }
        public string Name { get; set; }
        public string DoneInYear { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [NotMapped]
        public string? SuccessMessage { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
        [NotMapped]
        public List<Course> Courses { get; set; }
    }
}
