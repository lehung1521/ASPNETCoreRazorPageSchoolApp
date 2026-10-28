using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Item { get; set; } = new Instructor();

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

            _context.Instructors.Add(Item);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private void LoadLists()
        {
        // (khong co khoa ngoai)
        }
    }
}
