using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Item { get; set; } = default!;
        public SelectList? CourseList { get; set; }
        public SelectList? StudentList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (item == null) return NotFound();

            Item = item;
            LoadLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists();
                return Page();
            }

            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Enrollments.Any(e => e.EnrollmentID == Item.EnrollmentID)) return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }

        private void LoadLists()
        {
        CourseList = new SelectList(_context.Courses.ToList(), "CourseID", "Title", Item.CourseID);
        StudentList = new SelectList(_context.Students.ToList(), "StudentID", "FullName", Item.StudentID);
        }
    }
}
