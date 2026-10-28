using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;
using SchoolAppCoreRazor.Services;

namespace SchoolAppCoreRazor.Pages.Reports
{
    public class EnrollmentsModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly EnrollmentReportService _report;

        public EnrollmentsModel(SchoolContext context, EnrollmentReportService report)
        {
            _context = context;
            _report = report;
        }

        public SelectList? Departments { get; set; }
        public int? DepartmentId { get; set; }
        public bool Show { get; set; }

        public async Task OnGetAsync(int? departmentId, bool show = false)
        {
            DepartmentId = departmentId;
            Show = show;
            Departments = new SelectList(await _context.Departments.ToListAsync(), "DepartmentID", "Name", departmentId);
        }

        public async Task<IActionResult> OnGetPdfAsync(int? departmentId)
        {
            var bytes = await _report.RenderAsync(departmentId, "PDF");
            return File(bytes, "application/pdf");
        }

        public async Task<IActionResult> OnGetExcelAsync(int? departmentId)
        {
            var bytes = await _report.RenderAsync(departmentId, "EXCELOPENXML");
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EnrollmentReport.xlsx");
        }

        public async Task<IActionResult> OnGetWordAsync(int? departmentId)
        {
            var bytes = await _report.RenderAsync(departmentId, "WORDOPENXML");
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "EnrollmentReport.docx");
        }
    }
}
