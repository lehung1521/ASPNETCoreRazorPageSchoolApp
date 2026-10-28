using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Services
{
    public class EnrollmentReportRow
    {
        public string StudentName { get; set; } = "";
        public string CourseTitle { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public int Credits { get; set; }
        public decimal? Grade { get; set; }
    }

    /// <summary>Doc du lieu tu SchoolDB roi render file .rdlc ra PDF / Excel / Word.</summary>
    public class EnrollmentReportService
    {
        private readonly SchoolContext _context;
        private readonly IWebHostEnvironment _env;

        public EnrollmentReportService(SchoolContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // format: "PDF", "EXCELOPENXML" (.xlsx) hoac "WORDOPENXML" (.docx)
        public async Task<byte[]> RenderAsync(int? departmentId, string format = "PDF")
        {
            var query = _context.Enrollments.AsNoTracking().AsQueryable();
            if (departmentId.HasValue)
                query = query.Where(e => e.Course!.DepartmentID == departmentId.Value);

            var rows = await query
                .OrderBy(e => e.Student!.LastName)
                .Select(e => new EnrollmentReportRow
                {
                    StudentName = e.Student!.FirstName + " " + e.Student.LastName,
                    CourseTitle = e.Course!.Title,
                    DepartmentName = e.Course.Department!.Name,
                    Credits = e.Course.Credits,
                    Grade = e.Grade
                }).ToListAsync();

            var path = Path.Combine(_env.ContentRootPath, "Reports", "EnrollmentReport.rdlc");
            var report = new LocalReport();
            using (var stream = File.OpenRead(path))
            {
                report.LoadReportDefinition(stream);
            }
            report.DataSources.Add(new ReportDataSource("DataSet1", rows));
            return report.Render(format);
        }
    }
}
