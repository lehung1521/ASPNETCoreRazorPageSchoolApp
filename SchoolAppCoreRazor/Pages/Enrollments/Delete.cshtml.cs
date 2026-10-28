using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolContext _context;

        public DeleteModel(SchoolContext context)
        {
            _context = context;
        }

        public Enrollment Item { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Enrollments.Include(x => x.Course).Include(x => x.Student).FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (item == null) return NotFound();

            Item = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Enrollments.FindAsync(id);
            if (item != null)
            {
                try
                {
                    _context.Enrollments.Remove(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    _context.ChangeTracker.Clear();
                    Item = (await _context.Enrollments.Include(x => x.Course).Include(x => x.Student).AsNoTracking().FirstOrDefaultAsync(m => m.EnrollmentID == id))!;
                    ErrorMessage = "Cannot delete this record because other records still reference it.";
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
