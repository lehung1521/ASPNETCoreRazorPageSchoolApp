using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Item { get; set; } = new Enrollment();
        public SelectList? CourseList { get; set; }
        public SelectList? StudentList { get; set; }

        public void OnGet()
        {
            LoadLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists();
                return Page();
            }

            _context.Enrollments.Add(Item);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private void LoadLists()
        {
        CourseList = new SelectList(_context.Courses.ToList(), "CourseID", "Title", Item.CourseID);
        StudentList = new SelectList(_context.Students.ToList(), "StudentID", "FullName", Item.StudentID);
        }
    }
}
