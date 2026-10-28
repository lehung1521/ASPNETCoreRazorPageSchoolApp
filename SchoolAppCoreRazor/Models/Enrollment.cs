using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppCoreRazor.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [Display(Name = "Course")]
        public int? CourseID { get; set; }

        [Display(Name = "Student")]
        public int? StudentID { get; set; }

        [Column(TypeName = "decimal(3, 2)"), Range(0.0, 4.0)]
        public decimal? Grade { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
