using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppCoreRazor.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Budget { get; set; }

        [Display(Name = "Start Date")]
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
    }
}
