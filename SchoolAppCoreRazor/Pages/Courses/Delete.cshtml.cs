using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolContext _context;

        public DeleteModel(SchoolContext context)
        {
            _context = context;
        }

        public Course Item { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Courses.Include(x => x.Department).FirstOrDefaultAsync(m => m.CourseID == id);
            if (item == null) return NotFound();

            Item = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Courses.FindAsync(id);
            if (item != null)
            {
                try
                {
                    _context.Courses.Remove(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    _context.ChangeTracker.Clear();
                    Item = (await _context.Courses.Include(x => x.Department).AsNoTracking().FirstOrDefaultAsync(m => m.CourseID == id))!;
                    ErrorMessage = "Cannot delete this record because other records still reference it.";
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
