using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppCoreRazor.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }

        [Required, StringLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Hire Date")]
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
    }
}
