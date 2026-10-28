using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppCoreRazor.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required, StringLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Enrollment Date")]
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
    }
}
